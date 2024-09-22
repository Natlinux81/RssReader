using Domain.Entities;

namespace Domain.Interface
{
    public interface IRssFeedRepository : IGenericRepository<RssFeed>
    {
         // extra implementations
         Task<RssFeed?> GetByUrlAsync(string url);
    }
}