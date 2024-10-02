using Domain.Interface;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // // Read MariaDbSettings from configuration
            // var mariaDbSettings = configuration.GetRequiredSection("MariaDbSettings").Get<MariaDbSettings>();
            // var connectionString = mariaDbSettings?.ConnectionString;

            // // Register RssReaderDbContext with the DI container
            // services.AddDbContext<RssReaderDbContext>(options =>
            //     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Register repositories and unit of work
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRssFeedRepository, RssFeedRepository>();

            return services;
        }
    }
}