using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RssFeedRepository(RssReaderDbContext rssReaderDbContext) :  GenericRepository<RssFeed>(rssReaderDbContext), IRssFeedRepository
    {
        private readonly RssReaderDbContext _rssReaderDbContext = rssReaderDbContext;

        public async Task<RssFeed?> GetByUrlAsync(string url)
        {
            return await _rssReaderDbContext.RssFeeds.FirstOrDefaultAsync(x => x.Url == url);
        }
    }
}