using Application.Common.Results;
using Application.DTOs;
using Application.Errors;
using Application.Interfaces;
using Application.Models;
using Application.Validators;
using Domain.Interface;

namespace Application.Services;

public class RssFetchService(
    IUnitOfWork unitOfWork,
    IRssFeedRepository iRssFeedRepository,
    RssFeedRequestValidator rssFeedRequestValidator) : IRssFetchService
{
    public async Task<Result> AddRssFeed(RssFeedRequest rssFeedRequest, string feedUrl,
        CancellationToken cancellationToken)
    {
        // Check if RssFeedRequest is null. if yes, return error

        var validationResult = await rssFeedRequestValidator.ValidateAsync(rssFeedRequest, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return Result.Failure(RssFeedError.InvalidRssFeedUrl(errors));
        }

        // Check if RssFeed already exists
        var rssFeedExists = await iRssFeedRepository.GetByUrlAsync(feedUrl);

        if (rssFeedExists is not null) return Result.Failure(RssFeedError.RssFeedAlreadyExists);

        // Add RssFeed to database
        var rssFeed = await iRssFeedRepository.ReadRssFeed(new Uri(feedUrl), cancellationToken);

        await iRssFeedRepository.AddAsync(rssFeed);
        await unitOfWork.CommitAsync();
        return Result.Success("RSS-Feed added successfully");
    }

    public async Task<Result> GetAllRssFeeds()
    {
        var rssFeeds = await iRssFeedRepository.GetWithItemsAsync();

        if (rssFeeds.Count == 0) return Result.Failure(RssFeedError.RssFeedsNotFound);

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

        if (rssFeed == null) return Result.Failure(RssFeedError.RssFeedsNotFound); // if Feed not exist

        // Feed delete
        iRssFeedRepository.Delete(rssFeed);

        // save changes to database
        await unitOfWork.CommitAsync();

        return Result.Success("RSS-Feed deleted successfully");
    }

    public async Task<Result> UpdateFeedItemsAsync(CancellationToken cancellationToken)
    {
        var rssFeeds = await iRssFeedRepository.GetWithItemsAsync();

        if (rssFeeds.Count == 0) return Result.Failure(RssFeedError.RssFeedsNotFound);

        foreach (var existingFeed in rssFeeds)
        {
            if (existingFeed.Url == null) continue;
            var updatedFeed = await iRssFeedRepository.ReadRssFeed(new Uri(existingFeed.Url), cancellationToken);

            // New feed items that don't exist in the current items
            var newFeedItems = updatedFeed.FeedItems
                .Where(newItem => existingFeed.FeedItems.All(existingItem => existingItem.Link != newItem.Link))
                .ToList();

            // Obsolete feed items that no longer exist in the updated feed
            var obsoleteFeedItems = existingFeed.FeedItems
                .Where(existingItem => updatedFeed.FeedItems.All(newItem => newItem.Link != existingItem.Link))
                .ToList();

            // Add new feed items
            if (newFeedItems.Count != 0) existingFeed.FeedItems.AddRange(newFeedItems);

            // Remove obsolete feed items
            if (obsoleteFeedItems.Count != 0)
                foreach (var obsoleteItem in obsoleteFeedItems)
                    existingFeed.FeedItems.Remove(obsoleteItem);

            // Commit changes to the database if there are new or obsolete items
            if (newFeedItems.Count != 0 || obsoleteFeedItems.Count != 0) await unitOfWork.CommitAsync();
        }

        return Result.Success("RSS-Feeds updated successfully");
    }
}