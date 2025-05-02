using ZudBron.Domain.StaticModels.SmtpModel;
using Microsoft.OpenApi.Models;
using ZudBron.Infrastructure;

namespace ZudBron.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure Services
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure Middleware
        ConfigureMiddleware(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // SMTP settings
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

        // JWT settings (validate)
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret is missing in configuration.");

        // Add Controllers
        services.AddControllers();

        // Swagger settings
        services.AddEndpointsApiExplorer();
        services.AddInfrastructureRegisterServices(configuration);
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Bearer token"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        // CORS policy
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // Authentication, Authorization services
        services.AddAuthentication();
        services.AddAuthorization();
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        // Use HTTPS Redirection
        app.UseHttpsRedirection();

        // Enable Swagger (only in Development)
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Enable CORS
        app.UseCors("AllowAll");

        // Authentication and Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Map Controllers
        app.MapControllers();
    }
}
