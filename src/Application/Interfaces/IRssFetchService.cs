using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRssFetchService
    {
         Task<string?> RssFeedAdd(RssFeedRequest rssFeedRequest);
         Task<RssFeed> RssFeedGet(CancellationToken cancellationToken, Uri rssFeedUri);
    }
}