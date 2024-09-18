using Application.Common.Results;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRssFetchService
    {
         Task<Result> RssFeedAdd(RssFeedRequest rssFeedRequest);
         Task<Result<RssFeed>> RssFeedGet(CancellationToken cancellationToken, Uri rssFeedUri);
    }
}