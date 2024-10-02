namespace Application.DTOs
{
    public class RssFeedDto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? ChannelTitle { get; set; }

        public List<RssFeedItemDto> FeedItems {get; set; } = [];
    }
}