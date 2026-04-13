using ApplicationLayer.RepoInterfaces;
using DominLayer.Entites.AuthAndPermissions;
using InfrastructureLayer.Data;
using InfrastructureLayer.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Resturant.Authorization;
using ResturantDataAccessLayer.Seeding;
using System;
using System.Data;
using System.Globalization;
using System.Text;
using API_ERP_Layer.Middleware; // added
using ApplicationLayer.Base;
using ApplicationLayer.Common; // added for ResponseHandler and behaviors

//using ERP_System.Controllers.Inventory_Movements;


var builder = WebApplication.CreateBuilder(args);

// Bind JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
if (jwtSettings != null)
{
    builder.Services.AddSingleton(jwtSettings);
}

// Add services to the container.

builder.Services.AddControllers();
// Register MediatR handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationLayer.Features.AuthAndPermissions.Auth.Commands.Handlers.ForgotPasswordCommandHandler).Assembly));
// register pipeline behavior and ResponseHandler
builder.Services.AddSingleton<ResponseHandler>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Resturant API", Version = "v1" });

//    // JWT Bearer support in Swagger
//    var securityScheme = new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer abcdef12345\"",
//        Reference = new OpenApiReference
//        {
//            Type = ReferenceType.SecurityScheme,
//            Id = "Bearer"
//        }
//    };

//    c.AddSecurityDefinition("Bearer", securityScheme);
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        { securityScheme, new string[] { } }
//    });
//});

builder.Services.AddDbContext<ERPContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});

#region AllowCORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173"  // Vite
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion

// Register HttpClient for OAuth
builder.Services.AddHttpClient();
builder.Services.Configure<EmailSettingsDto>(builder.Configuration.GetSection("Email"));

// Register generic repository and unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Register Authorization Handlers
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services.AddIdentity<ApplicationUser, AspNetRole>(options =>
{
    // Example identity options (adjust as needed)
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ERPContext>()
    .AddDefaultTokenProviders();

var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
    {
    options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["accessToken"];
            return Task.CompletedTask;
}
    };
});

#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
            new CultureInfo("en-US"),
            new CultureInfo("de-DE"),
            new CultureInfo("fr-FR"),
            new CultureInfo("ar-EG")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

#endregion
builder.Services.AddScoped<IDataSeeder, DataSeeder>();

var app = builder.Build();

// إضافة هذا الكود لتشغيل الـ Seeder عند بدء التطبيق
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var seeder = services.GetRequiredService<IDataSeeder>();
        await seeder.SeedAsync();

        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Database seeded successfully");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resturant API v1"));
}

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion


app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseStaticFiles();

// Use custom exception middleware
app.UseMiddleware<ExceptionMiddleware>();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
