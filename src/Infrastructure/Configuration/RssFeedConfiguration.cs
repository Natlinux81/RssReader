using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class RssFeedConfiguration : IEntityTypeConfiguration<RssFeed>
{
    public void Configure(EntityTypeBuilder<RssFeed> builder)
    {
        builder.ToTable("RssFeeds");
        builder.HasKey(k => k.Id);
        builder.Property(x => x.Url);
        builder.Property(x => x.ChannelTitle);
        builder.HasMany(x => x.FeedItems)
            .WithOne(x => x.RssFeed)
            .HasForeignKey(x => x.RssFeedId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Users)
            .WithOne(x => x.RssFeed)
            .HasForeignKey(x => x.RssFeedId).OnDelete(DeleteBehavior.NoAction);
    }
}