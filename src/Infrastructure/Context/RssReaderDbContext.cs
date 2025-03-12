using Domain.Entities;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class RssReaderDbContext(DbContextOptions<RssReaderDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<RssFeed> RssFeeds { get; set; }
    public DbSet<RssFeedItem> RssFeedItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<RssFeed>().HasKey(k => k.Id);
        // modelBuilder.Entity<RssFeed>().ToTable("RssFeeds")
        //     .HasMany(feedItem => feedItem.FeedItems);
        // modelBuilder.Entity<RssFeedItem>().HasKey(k => k.Id);
        // modelBuilder.Entity<RssFeedItem>().ToTable("RssFeedItems")
        //     .HasOne(feed => feed.RssFeed)
        //     .WithMany(feed => feed.FeedItems)
        //     .HasForeignKey(feedItem => feedItem.RssFeedId);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RssReaderDbContext).Assembly);

        MockData.GetMockData(modelBuilder);
    }
}