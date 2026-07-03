using _0_Framework.Application;
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
using Microsoft.EntityFrameworkCore;
using ShopManagement.Configuration;
using ShopManagement.Infrastructure.EFCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LampShadeDb");

ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IFIleUploader, FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddTransient<ISmsService, SmsService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IHttpContextGetter, HttpContextGetter>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
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

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
