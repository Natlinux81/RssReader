using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Context
{
    public class RssReaderDbContextFactory : IDesignTimeDbContextFactory<RssReaderDbContext>
    {
        public RssReaderDbContext CreateDbContext(string[] args)

        {
            var connectionString = "";
            var optionBuilder = new DbContextOptionsBuilder<RssReaderDbContext>();
            optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new RssReaderDbContext(optionBuilder.Options);
        }
    }
}