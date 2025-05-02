using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZudBron.Application.IService.IAuthServices;
using ZudBron.Application.IService.IEmailServices;
using ZudBron.Application.IService.IFieldServices;
using ZudBron.Application.IService.ILocationServices;
using ZudBron.Application.IService.INotificationServices;
using ZudBron.Application.IService.ITokenServices;
using ZudBron.Application.IService.IUserServices;
using ZudBron.Domain.Abstractions;
using ZudBron.Infrastructure.Repositories.AuthRepositories;
using ZudBron.Infrastructure.Repositories.UserRepositories;
using ZudBron.Infrastructure.Services.AuthServices;
using ZudBron.Infrastructure.Services.EmailServices;
using ZudBron.Infrastructure.Services.FieldService;
using ZudBron.Infrastructure.Services.LocationServices;
using ZudBron.Infrastructure.Services.NotificationServices;
using ZudBron.Infrastructure.Services.TokenServices;
using ZudBron.Infrastructure.Services.UserServices;

namespace ZudBron.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureRegisterServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
              configuration.GetConnectionString("DefaultConnection") ??
              throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));


            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<INotificationService,NotificationService>();
            services.AddScoped<ILocationService,LocationService>();
            services.AddScoped<ISportFieldService, SportFieldService>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
