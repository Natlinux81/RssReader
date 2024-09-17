using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRssFetchService, Domain.RssFetchService>();
            
            return services;
        }
    }
}