using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTest;

public class UnitTest1
{
    [Fact]
    public void AddRssFeed_ShouldAddFeedAndItems()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RssReaderDbContext>()
            .UseInMemoryDatabase("TestDatabase") // Benenne die In-Memory-Datenbank
            .Options;

        using (var context = new RssReaderDbContext(options))
        {
            var rssFeed = new RssFeed
            {
                Id = 1,
                ChannelTitle = "Test Feed",
                FeedItems = new List<RssFeedItem>
                {
                    new() { Id = 1, Title = "First Item", RssFeedId = 1 },
                    new() { Id = 2, Title = "Second Item", RssFeedId = 1 }
                }
            };

            // Act
            context.RssFeeds.Add(rssFeed);
            context.SaveChanges();
        }

        // Assert
        using (var context = new RssReaderDbContext(options))
        {
            var feed = context.RssFeeds.Include(f => f.FeedItems).FirstOrDefault(f => f.Id == 1);
            Assert.NotNull(feed);
            Assert.Equal("Test Feed", feed.ChannelTitle);
            Assert.Equal(2, feed.FeedItems.Count);
            Assert.Equal("First Item", feed.FeedItems.First().Title);
            Assert.Equal("Second Item", feed.FeedItems.Last().Title);
        }
    }
}