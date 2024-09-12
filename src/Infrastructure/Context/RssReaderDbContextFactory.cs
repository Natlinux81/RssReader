using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    public class RssReaderDbContextFactory : IDesignTimeDbContextFactory<RssReaderDbContext>
    {
        public RssReaderDbContext CreateDbContext(string[] args)

        {
            // Build the configuration manually
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../API"))
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<RssReaderDbContextFactory>()
            .Build();

            // Read MariaDbSettings from configuration
            var mariaDbSettings = configuration.GetRequiredSection("MariaDbSettings").Get<MariaDbSettings>();
            var connectionString = mariaDbSettings?.ConnectionString;
            var optionBuilder = new DbContextOptionsBuilder<RssReaderDbContext>();
            optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new RssReaderDbContext(optionBuilder.Options);
        }
    }
}