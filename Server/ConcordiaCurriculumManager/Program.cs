using ConcordiaCurriculumManager.Filters;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

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

        if (env.IsProduction())
        {
            builder.Configuration.AddEnvironmentVariables();
        }

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

        builder.Services.AddAuthorizationHandlers();
        builder.Services.AddAuthorization(options => options.AddPolicies());

        var dbSetting = builder.Configuration
                            .GetSection(DatabaseSettings.SectionName)
                            .Get<DatabaseSettings>();

        builder.Services.AddOptions<DatabaseSettings>()
                       .Bind(builder.Configuration.GetSection(DatabaseSettings.SectionName))
                       .Validate(dbSettings => dbSettings.ConnectionString is not null);

        var dbDataSource = new NpgsqlDataSourceBuilder(dbSetting!.ConnectionString).Build();
        builder.Services.AddDbContext<CCMDbContext>((options) =>
        {
            options.UseNpgsql(dbDataSource);
        });

        builder.Host.UseSerilog((context, logger) =>
        {
            logger
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext();
        });


        var senderEmailSettings = builder.Configuration.GetSection(SenderEmailSettings.SectionName).Get<SenderEmailSettings>();

        if (senderEmailSettings is null || string.IsNullOrWhiteSpace(senderEmailSettings.SenderSMTPHost) || string.IsNullOrWhiteSpace(senderEmailSettings.SenderEmail) || string.IsNullOrWhiteSpace(senderEmailSettings.SenderPassword) || senderEmailSettings.SenderSMTPPort <= 0)
        {
            throw new ArgumentException("Invalid Sender Email Settings: SenderSMTPHost, SenderEmail, and SenderPassword are mandatory");
        }

        builder.Services.AddOptions<SenderEmailSettings>()
                       .Bind(builder.Configuration.GetSection(SenderEmailSettings.SectionName));

        AddServices(builder.Services);

        builder.Services.AddMemoryCache();
        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionHandlerFilter>();
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

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
        app.UseMiddleware<EnrichLogContextMiddleware>();

        app.MapControllers();

        app.Run();
    }

    public static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IInputHasherService, InputHasherService>();
        services.AddSingleton<ICacheService<string>, CacheService<string>>();
        services.AddSingleton<IEmailService, EmailService>();

        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseGroupingService, CourseGroupingService>();
        services.AddScoped<IDossierService, DossierService>();
        services.AddScoped<IDossierReviewService, DossierReviewService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseGroupingRepository, CourseGroupingRepository>();
        services.AddScoped<IDossierRepository, DossierRepository>();
        services.AddScoped<IDossierReviewRepository, DossierReviewRepository>();
        services.AddScoped<ICourseIdentifiersRepository, CourseIdentifiersRepository>();
    }
}
