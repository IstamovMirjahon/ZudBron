using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZudBron.Application.IService.IUserServices;
using ZudBron.Domain.Abstractions;
using ZudBron.Infrastructure.Repositories.UserRepositories;
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

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
