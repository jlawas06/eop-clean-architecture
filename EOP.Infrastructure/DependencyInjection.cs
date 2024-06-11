using EOP.Application.Interfaces;
using EOP.Infrastructure.Context;
using EOP.Infrastructure.Providers;
using EOP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EOP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //DB Context
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<IJWTProvider, JWTProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
