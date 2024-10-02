using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RssFeedRepository(HttpClient httpClient, RssReaderDbContext rssReaderDbContext) : GenericRepository<RssFeed>(rssReaderDbContext), IRssFeedRepository
    {
        private readonly RssReaderDbContext _rssReaderDbContext = rssReaderDbContext;

        public async Task<RssFeed?> GetByUrlAsync(string url)
        {
            return await _rssReaderDbContext.RssFeeds
                                 .Include(r => r.FeedItems)  // Optionales Eager Loading, falls benötigt
                                 .FirstOrDefaultAsync(r => r.Url == url);
        }

        public async Task<RssFeed> ReadRssFeed(Uri rssFeedUri, CancellationToken cancellationToken)
        {
            // RSS-Feed call
            var response = await httpClient.GetAsync(rssFeedUri, cancellationToken);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using XmlReader reader = XmlReader.Create(stream);

            SyndicationFeed feed = SyndicationFeed.Load(reader);

            var rssFeed = new RssFeed()
            {
                Url = rssFeedUri.ToString(),
                ChannelTitle = feed.Title.Text,
                FeedItems = feed.Items.Select(item => new RssFeedItem()
                {
                    Title = item.Title.Text,
                    Description = item.Summary?.Text,
                    Link = item.Links[0].Uri.ToString(),
                    PublishDate = item.PublishDate.DateTime,
                    ImageUrl = GetImageUrlFromItemHelper.GetImageUrlFromItem(item)
                }).ToList()
            };

            return rssFeed;

        }

        public async Task<List<RssFeed>> GetByWithItemsAsync()
        {
            return await _rssReaderDbContext.RssFeeds.
            Include(r => r.FeedItems).
            ToListAsync();
        }
    }
}