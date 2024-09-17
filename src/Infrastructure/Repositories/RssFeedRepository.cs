using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RssFeedRepository(RssReaderDbContext rssReaderDbContext) : GenericRepository<RssFeed>(rssReaderDbContext), IRssFeedRepository
    {
        public async Task<RssFeed> GetByUrl(string url)
        {
            return await rssReaderDbContext.RssFeeds.FirstOrDefaultAsync(x => x.Url == url);
        }
    }
}