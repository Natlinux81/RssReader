using Application.Common.Results;
using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Domain.Interface;

namespace Application.Services;

public class RssFetchService(IUnitOfWork unitOfWork, IRssFeedRepository iRssFeedRepository) : IRssFetchService
{
    public async Task<Result> AddRssFeed(RssFeedRequest? rssFeedRequest, string feedUrl, CancellationToken cancellationToken)
    {
        // Check if RssFeedRequest is null. if yes, return error
        if (rssFeedRequest == null)
        {
            return Result.Failure(RssFeedError.InvalidRssFeedRequest);
        }
        // Check if RssFeed already exists
        var rssFeedExists = await iRssFeedRepository.GetByUrlAsync(feedUrl);

        if (rssFeedExists is not null)
        {
            return Result.Failure(RssFeedError.RssFeedAlreadyExists);
        }

        // Add RssFeed to database
        var rssFeed = await iRssFeedRepository.ReadRssFeed(new Uri(feedUrl), cancellationToken);

        await iRssFeedRepository.AddAsync(rssFeed);
        await unitOfWork.CommitAsync();
        return Result.Success("RSS-Feed added successfully");
    }

    public async Task<Result> GetAllRssFeeds()
    {
        var rssFeeds = await iRssFeedRepository.GetWithItemsAsync();

        if (rssFeeds.Count == 0)
        {
            return Result.Failure(RssFeedError.InvalidRssFeedRequest);
        }

        var rssFeedDtos = rssFeeds.Select(r => new RssFeedDto
        {
            Id = r.Id,
            Url = r.Url,
            ChannelTitle = r.ChannelTitle,
            FeedItems = r.FeedItems.Select(fi => new RssFeedItemDto
            {
                Id = fi.Id,
                Title = fi.Title,
                Description = fi.Description,
                Link = fi.Link,
                PublishDate = fi.PublishDate,
                ImageUrl = fi.ImageUrl
            }).ToList()
        }).ToList();

        return Result.Success(rssFeedDtos);
    }

    public async Task<Result> DeleteRssFeed(int id)
    {
        // check if Feed exist
        var rssFeed = await iRssFeedRepository.GetByIdAsync(id);

        if (rssFeed == null)
        {
            return Result.Failure(RssFeedError.RssFeedNotFound); // if Feed not exist
        }

        // Feed delete
        iRssFeedRepository.Delete(rssFeed);

        // save changes to database
        await unitOfWork.CommitAsync();

        return Result.Success("RSS-Feed deleted successfully");
    }
}