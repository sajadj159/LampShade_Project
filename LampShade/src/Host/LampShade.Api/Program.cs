using AccountManagement.Infrastructure;
using AccountManagement.Presentation.Controllers.Write;
using Amazon.Runtime;
using Amazon.S3;
using BlogManagement.Infrastructure;
using BlogManagement.Presentation.Controllers.Write;
using CommentManagement.Infrastructure;
using CommentManagement.Presentation.Controllers.Write;
using DiscountManagement.Infrastructure;
using DiscountManagement.Presentation.Controllers.Write;
using InventoryManagement.Infrastructure;
using InventoryManagement.Presentation.Controllers.Write;
using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Domain;
using LampShade.BuildingBlocks.Infrastructure;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using ShopManagement.Infrastructure;
using ShopManagement.Presentation.Controllers.Write;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

var objectStorageOptions = builder.Configuration
    .GetSection(ObjectStorageOptions.SectionName)
    .Get<ObjectStorageOptions>()
    ?? throw new InvalidOperationException("Object storage configuration is missing.");

if (string.IsNullOrWhiteSpace(objectStorageOptions.ServiceUrl)
    || string.IsNullOrWhiteSpace(objectStorageOptions.BucketName)
    || string.IsNullOrWhiteSpace(objectStorageOptions.AccessKey)
    || string.IsNullOrWhiteSpace(objectStorageOptions.SecretKey))
{
    throw new InvalidOperationException("Object storage configuration is incomplete.");
}

// Modules: each one registers its own DbContext (own schema), repositories, queries, public API and handlers.
builder.Services.AddModules(builder.Configuration,
    new ShopModule(),
    new DiscountModule(),
    new InventoryModule(),
    new BlogModule(),
    new CommentModule(),
    new AccountModule());

// Shared kernel services used by every module.
builder.Services.Configure<ObjectStorageOptions>(builder.Configuration.GetSection(ObjectStorageOptions.SectionName));
builder.Services.AddSingleton<IAmazonS3>(_ => new AmazonS3Client(
    new BasicAWSCredentials(objectStorageOptions.AccessKey, objectStorageOptions.SecretKey),
    new AmazonS3Config
    {
        ServiceURL = objectStorageOptions.ServiceUrl,
        ForcePathStyle = true,
        AuthenticationRegion = "us-east-1"
    }));
builder.Services.AddHostedService<ObjectStorageInitializer>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IHtmlSanitizer, HtmlSanitizerService>();
builder.Services.AddTransient<IFileUploader, S3FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddTransient<ISmsService, SmsService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextGetter, HttpContextGetter>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/write/Account/login";
        options.LogoutPath = "/api/write/Account/logout";
        options.AccessDeniedPath = "/api/write/Account/login";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        // An API answers with status codes instead of redirecting to a login page.
        options.Events.OnRedirectToLogin = context => { context.Response.StatusCode = StatusCodes.Status401Unauthorized; return Task.CompletedTask; };
        options.Events.OnRedirectToAccessDenied = context => { context.Response.StatusCode = StatusCodes.Status403Forbidden; return Task.CompletedTask; };
    });

// Controllers live in each module's Presentation assembly.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AccountController).Assembly)
    .AddApplicationPart(typeof(ArticleController).Assembly)
    .AddApplicationPart(typeof(CommentController).Assembly)
    .AddApplicationPart(typeof(CustomerDiscountController).Assembly)
    .AddApplicationPart(typeof(InventoryController).Assembly)
    .AddApplicationPart(typeof(ProductController).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

try
{
    await app.Services.InitializeModulesAsync();
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "An error occurred while initializing the modules");
    throw;
}

// Rule violations raised below the controllers become 400 responses with a readable message.
app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    if (exception is AppValidationException or DomainException)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(new { isSucceeded = false, message = exception.Message });
        return;
    }

    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await context.Response.WriteAsJsonAsync(new { isSucceeded = false, message = "An unexpected error occurred." });
}));

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "LampShade API v1");
    options.RoutePrefix = "swagger";
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
