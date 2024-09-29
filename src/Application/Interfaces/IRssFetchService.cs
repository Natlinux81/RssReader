using Application.Common.Results;
using Application.Models;

namespace Application.Interfaces
{
    public interface IRssFetchService
    {
         Task<Result> AddRssFeed(RssFeedRequest rssFeedRequest, string feedUrl, CancellationToken cancellationToken);
         Task<Result> GetAllRssFeeds();
    }
}