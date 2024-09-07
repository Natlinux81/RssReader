using System.Reflection.Metadata;

namespace Domain.Entities
{
    public class RssFeedItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public Blob Image { get; set; }

        public int RssFeedId { get; set; }
        public RssFeed RssFeed { get; set; }
    }
}