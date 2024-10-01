using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
  public class RssReaderDbContext(DbContextOptions<RssReaderDbContext> options) : DbContext(options)
  {
    public DbSet<RssFeed> RssFeeds { get; set; }
    public DbSet<RssFeedItem> RssFeedItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<RssFeed>()
            .HasMany(r => r.FeedItems)
            .WithOne(f => f.RssFeed)
            .HasForeignKey(f => f.RssFeedId);
    }
  }
}