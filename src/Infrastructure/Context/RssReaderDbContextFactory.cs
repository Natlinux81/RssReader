using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public static class DbContextOptionsFactory
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
            // Read PostgreSQLSettings from configuration
            var mariaDbSettings = configuration.GetRequiredSection("PostgreSQLSettings").Get<PostgreSqlSettings>();
            var connectionString = mariaDbSettings?.ConnectionString;
            var optionBuilder = new DbContextOptionsBuilder<RssReaderDbContext>();
            if (connectionString != null)
                optionBuilder.UseNpgsql(connectionString);

            return new RssReaderDbContext(optionBuilder.Options);
        }
    }
}