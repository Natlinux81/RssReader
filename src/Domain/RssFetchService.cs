using System.ServiceModel.Syndication;
using System.Xml;
using Domain.Entities;

namespace Domain;

public class RssFetchService
{
    private readonly HttpClient _httpClient;
    
    public RssFetchService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public virtual async Task<RssFeed> Fetch(CancellationToken cancellationToken, Uri rssFeedUri)
    {
        // RSS-Feed call
        var response = await _httpClient.GetAsync(rssFeedUri);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using XmlReader reader = XmlReader.Create(stream);

        SyndicationFeed feed = SyndicationFeed.Load(reader);

        var rssItems = new RssFeed()
        {
            Url = rssFeedUri.ToString(),
            ChannelTitle = feed.Title.Text,
            FeedItems = feed.Items.Select(item => new RssFeedItem()
            {
                Title = item.Title.Text,
                Description = item.Summary?.Text,
                Link = item.Links[0].Uri.ToString(),
                PublishDate = item.PublishDate.DateTime,
                ImageUrl = item.Links.FirstOrDefault(link => link.MediaType?.StartsWith("image") == true)?.Uri.ToString()
            }).ToList()
        };

        return rssItems;
    }
}
