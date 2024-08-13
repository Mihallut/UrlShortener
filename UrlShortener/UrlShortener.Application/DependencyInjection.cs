using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UrlShortener.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
