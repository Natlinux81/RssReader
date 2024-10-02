using Domain.Entities;

namespace Application.DTOs
{
    public class RssFeedItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }

        public int RssFeedId { get; set; }
        public RssFeed? RssFeed { get; set; }
    }
}