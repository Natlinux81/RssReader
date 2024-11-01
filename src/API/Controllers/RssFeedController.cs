using API.Extension;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RssFeedController(IRssFetchService rssFetchService) : BaseApiController
{
    [HttpPost]
    public async Task<IResult> AddRssFeed([FromQuery] CancellationToken cancellationToken,
        [FromBody] RssFeedRequest rssFedRequest, string feedUrl)
    {
        // add rss feed to database
        var response = await rssFetchService.AddRssFeed(rssFedRequest, feedUrl, cancellationToken);
        // error Status
        return response.ToHttpResponse();
    }

    [HttpGet]
    public async Task<IResult> GetAllRssFeeds()
    {
        // get all rss feeds from database
        var response = await rssFetchService.GetAllRssFeeds();
        // error Status
        return response.ToHttpResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteRssFeed(int id)
    {
        // Delete RSS feed by ID
        var response = await rssFetchService.DeleteRssFeed(id);
        // error Status
        return response.ToHttpResponse();
    }

    [HttpPut("update")]
    public async Task<IResult> UpdateRssFeedItems(int id, [FromQuery] CancellationToken cancellationToken)
    {
        // update RSSFeedItems
        var response = await rssFetchService.UpdateFeedItemsAsync(cancellationToken);
        // error Status
        return response.ToHttpResponse();
    }
}