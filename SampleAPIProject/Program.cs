using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MLMProject.Application.Interfaces;
using MLMProject.Application.IRepository;
using MLMProject.Application.IService;
using MLMProject.Application.Services;
using MLMProject.Infrastructure.Extentions;
using MLMProject.Infrastructure.Repository;
using MLMProject.Infrastructure.Seeders;
using MLMProject.Profiles;
using NLog;
using NLog.Web;
using OfficeOpenXml;
using System;
using System.Text;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

        // Add security definitions
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    });

    // Configure JWT Authentication
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IEmailService, EmailService>();

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();

    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


    // Add AutoMapper and validate configuration
    var mapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<MappingProfile>();
        //cfg.ValidateInlineMaps = false; // Disable the validation if necessary
    });

    // Validate the configuration
    //mapperConfig.AssertConfigurationIsValid();

    builder.Services.AddSingleton<IMapper>(sp => mapperConfig.CreateMapper());

    builder.Services.AddInfrastructure(builder.Configuration);

    // Add session services
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(5);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    // Add NLog to .NET Core
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var app = builder.Build();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IUserSeeders>();
    await seeder.Seed();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else // In Production, only enable Swagger for authorized users
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
        });

        // Optionally restrict access
        app.UseWhen(ctx => ctx.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
        {
            appBuilder.UseAuthentication(); // Enforce authentication for Swagger
            appBuilder.UseAuthorization();  // Ensure authorization is applied
        });
    }

    bool enableSwagger = builder.Configuration.GetValue<bool>("EnableSwagger");

    if (enableSwagger)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
        });
    }

    // Enable CORS
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    // Add session middleware
    app.UseSession(); // Ensure this is before UseRouting

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
