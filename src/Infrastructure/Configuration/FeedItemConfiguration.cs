using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class FeedItemConfiguration: IEntityTypeConfiguration<RssFeedItem>
{
    public void Configure(EntityTypeBuilder<RssFeedItem> builder)
    {
        builder.ToTable("RssFeedItems");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title);
        builder.Property(x => x.Link);
        builder.Property(x => x.Description);
        builder.Property(x => x.PublishDate);
        builder.Property(x => x.ImageUrl);
        builder.HasOne(x => x.RssFeed)
            .WithMany(x => x.FeedItems)
            .HasForeignKey(x => x.RssFeedId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}