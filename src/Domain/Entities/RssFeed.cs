namespace Domain.Entities;

public class RssFeed
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public string? ChannelTitle { get; set; }
    
    public List<RssFeedItem> FeedItems { get; set; } = [];
}
