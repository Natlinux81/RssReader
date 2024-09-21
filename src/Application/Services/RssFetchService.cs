using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using Application;
using Application.Common.Results;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;

namespace Domain;

public class RssFetchService(HttpClient httpClient, IUnitOfWork unitOfWork, IRssFeedRepository rssFeedRepository) : IRssFetchService
{
    public virtual async Task<Result<RssFeed>> RssFeedGet(CancellationToken cancellationToken, Uri rssFeedUri)
    {
        // RSS-Feed call
        var response = await httpClient.GetAsync(rssFeedUri, cancellationToken);
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
                ImageUrl = GetImageUrlFromItem(item)
            }).ToList()
        };

        return Result.Success(rssItems);
    }

    private string? GetImageUrlFromItem(SyndicationItem item)
{
    // Extract image URL from an enclosure tag (often in RSS 2.0 feeds)
    var enclosure = item.Links.FirstOrDefault(link => link.RelationshipType == "enclosure" && link.MediaType?.StartsWith("image") == true);
    if (enclosure != null)
    {
        return enclosure.Uri.ToString();
    }

    // Alternative method: Extract image from media:content or media:thumbnail
    foreach (var extension in item.ElementExtensions)
    {
        if (extension.OuterName == "content" || extension.OuterName == "thumbnail")
        {
            var xmlElement = extension.GetObject<XElement>();
            var urlAttribute = xmlElement.Attribute("url");
            if (urlAttribute != null)
            {
                return urlAttribute.Value;
            }
        }
    }

     // Method for content:encoded tags
    var contentEncoded = item.ElementExtensions.FirstOrDefault(ext => 
        ext.OuterName == "encoded" && ext.OuterNamespace == "http://purl.org/rss/1.0/modules/content/");

    if (contentEncoded != null)
    {
        var htmlContent = contentEncoded.GetObject<XElement>().Value;
        
        // HTML parsing to find the image tag
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(htmlContent);

        var imgTag = doc.DocumentNode.SelectSingleNode("//img");
        if (imgTag != null)
        {
            var src = imgTag.GetAttributeValue("src", null);
            return src; 
        }
    }


    return null;
}

    public async Task<Result> RssFeedAdd(RssFeedRequest rssFeedRequest)
    {
        // Check if RssFeedRequest is null. if yes, return error
        if (rssFeedRequest == null)
        {
            return Result.Failure(RssFeedError.InvalidRssFeedRequest);
        }

        // Check if RssFeed already exists
        var rssFeedExists = await rssFeedRepository.GetByUrlAsync(rssFeedRequest.Url);

        if (rssFeedExists is not null)
        {
            return Result.Failure(RssFeedError.RssFeedAlreadyExists);
        }

        // Add RssFeed to database
        var rssFeed = new RssFeed()
        {
            Url = rssFeedRequest.Url,
            ChannelTitle = rssFeedRequest.ChannelTitle,
            FeedItems = rssFeedRequest.FeedItems.Select(item => new RssFeedItem()
            {
                Title = item.Title,
                Description = item.Description,
                Link = item.Link,
                PublishDate = item.PublishDate,
                ImageUrl = item.ImageUrl
            }).ToList()
        };

        await rssFeedRepository.AddAsync(rssFeed);
        await unitOfWork.CommitAsync();
        return Result.Success("RSS-Feed added successfully");
    }

}
