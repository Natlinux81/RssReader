namespace Application.Models;

public record RssFeedRequest(string Url, string ChannelTitle, List<RssFeedItemRequest> FeedItems);