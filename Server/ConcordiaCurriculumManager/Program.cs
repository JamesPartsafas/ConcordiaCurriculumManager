using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Reflection;
using System.Text;

namespace ConcordiaCurriculumManager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var env = builder.Environment;

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false);

        var identitySettings = builder.Configuration
                            .GetSection(IdentitySettings.SectionName)
                            .Get<IdentitySettings>();

        builder.Services.AddOptions<IdentitySettings>()
                        .Bind(builder.Configuration.GetSection(IdentitySettings.SectionName))
                        .Validate(identitySettings => identitySettings.Issuer is not null && identitySettings.Audience is not null && identitySettings.SecurityAlgorithms is not null && identitySettings.Key is not null);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = identitySettings!.Issuer,
                    ValidAudience = identitySettings!.Audience,
                    ValidAlgorithms = new[] { identitySettings!.SecurityAlgorithms },
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings!.Key))
                };

                options.Events = new TokenValidation();
            });

        builder.Services.AddAuthorization(options => options.AddPolicies());

        var dbSetting = builder.Configuration
                            .GetSection(DatabaseSettings.SectionName)
                            .Get<DatabaseSettings>();

        builder.Services.AddOptions<DatabaseSettings>()
                       .Bind(builder.Configuration.GetSection(DatabaseSettings.SectionName))
                       .Validate(dbSettings => dbSettings.ConnectionString is not null);

        builder.Services.AddDbContext<CCMDbContext>(options =>
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(dbSetting!.ConnectionString);
            options.UseNpgsql(dataSourceBuilder.Build());
        });

        AddServices(builder.Services);

        builder.Services.AddMemoryCache();
        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        if (env.IsDevelopment())
        {
            builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.ExampleFilters();
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the access obtained from Authentication endpoint as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        var corsSettings = builder.Configuration.GetSection(CorsSettings.SectionName).Get<CorsSettings>();

        if (corsSettings is null || !Uri.IsWellFormedUriString(corsSettings.AllowedWebsite, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid Allowed Website url");
        }

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors(policy => policy.SetIsOriginAllowed(origin => origin.StartsWith(corsSettings.AllowedWebsite, StringComparison.OrdinalIgnoreCase))
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
                                    .Build());

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IInputHasherService, InputHasherService>();
        services.AddSingleton<ICacheService<string>, CacheService<string>>();

        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IDossierService, DossierService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IDossierRepository, DossierRepository>();
    }
}
