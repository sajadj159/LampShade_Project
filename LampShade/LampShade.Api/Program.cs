using _0_Framework.Application;
using Amazon.Runtime;
using Amazon.S3;
using _0_Framework.Application.Email;
using _0_Framework.Application.SMS;
using _0_Framework.Application.ZarinPal;
using AccountManagement.Configuration;
using AccountManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Configuration;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Configuration;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Configuration;
using InventoryManagement.Infrastructure.EFCore;
using LampShade.Api;
using LampShade.Api.SeedData;
using LampShade.Api.Storage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Configuration;
using ShopManagement.Infrastructure.EFCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LampShadeDb");
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

ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);

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
builder.Services.AddTransient<IFIleUploader, S3FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddTransient<ISmsService, SmsService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextGetter, HttpContextGetter>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(AccountManagement.Application.Features.Accounts.Commands.Register.RegisterCommandHandler).Assembly,
    typeof(CommentManagement.Application.Features.Comments.Commands.AddComment.AddCommentCommandHandler).Assembly,
    typeof(BlogManagement.Application.Features.Articles.Commands.CreateArticle.CreateArticleCommandHandler).Assembly,
    typeof(DiscountManagement.Application.Features.CustomerDiscounts.Commands.DefineCustomerDiscount.DefineCustomerDiscountCommandHandler).Assembly,
    typeof(ShopManagement.Application.Features.Products.Commands.CreateProduct.CreateProductCommandHandler).Assembly,
    typeof(InventoryManagement.Application.Features.Inventories.Commands.CreateInventory.CreateInventoryCommandHandler).Assembly,
    typeof(LampShade.ReadModel.Application.ReadModelAssemblyMarker).Assembly));

// Configure Cookie Authentication
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
    });

builder.Services.AddControllers();
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var shopContext = services.GetRequiredService<ShopContext>();
        shopContext.Database.Migrate();

        var inventoryContext = services.GetRequiredService<InventoryContext>();
        inventoryContext.Database.Migrate();

        var commentContext = services.GetRequiredService<CommentContext>();
        commentContext.Database.Migrate();

        var blogContext = services.GetRequiredService<BlogContext>();
        blogContext.Database.Migrate();

        var discountContext = services.GetRequiredService<DiscountContext>();
        discountContext.Database.Migrate();

        var accountContext = services.GetRequiredService<AccountContext>();
        accountContext.Database.Migrate();
        accountContext.SeedDefaultRoles();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations");
        throw;
    }
}

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "LampShade API v1");
        options.RoutePrefix = "swagger";
});

// Development-mode debugging features
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


