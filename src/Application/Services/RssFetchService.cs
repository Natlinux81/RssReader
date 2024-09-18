﻿using System.ServiceModel.Syndication;
using System.Xml;
using Application;
using Application.Common.Results; 
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;

namespace Domain;

public class RssFetchService (HttpClient httpClient, IUnitOfWork unitOfWork, IRssFeedRepository rssFeedRepository) : IRssFetchService 
{
        public virtual async Task<Result<RssFeed>> RssFeedGet(CancellationToken cancellationToken, Uri rssFeedUri)
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

        return Result.Success(rssItems);
    }
    public async Task<Result> RssFeedAdd(RssFeedRequest rssFeedRequest)
    {
        if (rssFeedRequest == null)
        {
            return Result.Failure(RssFeedError.InvalidRssFeedRequest);
        }

        var rssFeedExists = await rssFeedRepository.GetByUrlAsync(rssFeedRequest.Url);

        if (rssFeedExists is not null)
        {
            return Result.Failure(RssFeedError.RssFeedAlreadyExists);
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
        return Result.Success("RSS-Feed added successfully");
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
