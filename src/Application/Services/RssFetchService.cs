using System.ServiceModel.Syndication;
using System.Xml;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;

namespace Domain;

public class RssFetchService (HttpClient httpClient, IUnitOfWork unitOfWork, IRssFeedRepository rssFeedRepository) : IRssFetchService 
{
    public async Task<string?> RssFeedAdd(RssFeedRequest rssFeedRequest)
    {
        if (rssFeedRequest == null)
        {
            throw new ArgumentNullException(nameof(rssFeedRequest));
        }

        var rssFeedExists = await rssFeedRepository.GetByUrlAsync(rssFeedRequest.Url);

        if (rssFeedExists is not null)
        {
            throw new Exception("RSS-Feed already exists");
        }
        
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
        return "RSS-Feed added";
    }

    public virtual async Task<RssFeed> RssFeedGet(CancellationToken cancellationToken, Uri rssFeedUri)
    {
        if (rssFeedUri == null)
        {
            throw new Exception("RSS-Feed URI cannot be empty");
        }

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
                ImageUrl = item.Links.FirstOrDefault(link => link.MediaType?.StartsWith("image") == true)?.Uri.ToString()
            }).ToList()
        };

        return rssItems;
    }


}

// public class RssFetchService(HttpClient httpClient)
// {   

//     public virtual async Task<RssFeed> Fetch(CancellationToken cancellationToken, Uri rssFeedUri)
//     {
//         // RSS-Feed call
//         var response = await httpClient.GetAsync(rssFeedUri, cancellationToken);
//         response.EnsureSuccessStatusCode();

//         using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
//         using XmlReader reader = XmlReader.Create(stream);

//         SyndicationFeed feed = SyndicationFeed.Load(reader);

//         var rssItems = new RssFeed()
//         {
//             Url = rssFeedUri.ToString(),
//             ChannelTitle = feed.Title.Text,
//             FeedItems = feed.Items.Select(item => new RssFeedItem()
//             {
//                 Title = item.Title.Text,
//                 Description = item.Summary?.Text,
//                 Link = item.Links[0].Uri.ToString(),
//                 PublishDate = item.PublishDate.DateTime,
//                 ImageUrl = item.Links.FirstOrDefault(link => link.MediaType?.StartsWith("image") == true)?.Uri.ToString()
//             }).ToList()
//         };

//         return rssItems;
//     }
// }
