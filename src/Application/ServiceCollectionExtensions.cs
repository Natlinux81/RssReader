using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRssFetchService, RssFetchService>();
            // services.AddHostedService<RssFeedUpdateService>();

            return services;
        }
    }
}