using Microsoft.Extensions.DependencyInjection;

namespace EOP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddAutoMapper(assembly);
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly)
            );

            return services;
        }
    }
}
