using Application.Common.Results;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRssFetchService
    {
         Task<Result> AddRssFeed(RssFeedRequest rssFeedRequest, string feedUrl, CancellationToken cancellationToken);
    }
}