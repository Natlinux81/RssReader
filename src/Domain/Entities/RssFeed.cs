namespace Domain.Entities;

public class RssFeed
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    
    public List<RssFeedItem> Items { get; set; }
}
