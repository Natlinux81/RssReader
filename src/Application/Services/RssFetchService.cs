using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using Application;
using Application.Common.Results;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Utilities;

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
                ImageUrl = GetImageUrlFromItemHelper.GetImageUrlFromItem(item)
            }).ToList()
        };

        return Result.Success(rssItems);
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
