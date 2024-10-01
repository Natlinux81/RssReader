using Application;
using Application.Common.Results;
using Application.Interfaces;
using Application.Models;
using Domain.Interface;


namespace Domain;

public class RssFetchService(IUnitOfWork unitOfWork, IRssFeedRepository iRssFeedRepository) : IRssFetchService
{
    public async Task<Result> AddRssFeed(RssFeedRequest rssFeedRequest , string feedUrl, CancellationToken cancellationToken)
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
        var rssFeed = await iRssFeedRepository.ReadRssFeed( new Uri(feedUrl),cancellationToken);
        
        await iRssFeedRepository.AddAsync(rssFeed);
        await unitOfWork.CommitAsync();
        return Result.Success("RSS-Feed added successfully");
    } 

    public async Task<Result> GetAllRssFeeds()
    {       
        var rssFeeds = await iRssFeedRepository.GetAllAsync();

        if (rssFeeds == null || rssFeeds.Count == 0)
        {
            return Result.Failure(RssFeedError.InvalidRssFeedRequest);
        }       
        
        return Result.Success(rssFeeds);
    }

public async Task<Result> DeleteRssFeed(int id)
{
    // Überprüfen, ob der Feed existiert
    var rssFeed = await iRssFeedRepository.GetByIdAsync(id);

    if (rssFeed == null)
    {
        return Result.Failure(RssFeedError.RssFeedNotFound); // Falls der Feed nicht existiert
    }

    // Feed löschen
    iRssFeedRepository.Delete(rssFeed);

    // Änderungen in der Datenbank speichern
    await unitOfWork.CommitAsync();

    return Result.Success("RSS-Feed deleted successfully");
}
}