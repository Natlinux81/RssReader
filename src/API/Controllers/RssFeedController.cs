using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RssFeedController(IRssFetchService rssFetchService) : BaseApiController
    {
        [HttpPost]
        public async Task<IResult> AddRssFeed([FromQuery] CancellationToken cancellationToken, [FromBody] RssFeedRequest rssFedRequest, string feedUrl)
        {
            // add rss feed to database
            var response = await rssFetchService.AddRssFeed(rssFedRequest, feedUrl, cancellationToken);

            // check if response is failure
            if (response.IsFailure)
            {
                return Results.BadRequest(response);
            }
            return Results.Ok(response);
        }

        [HttpGet]
        public async Task<IResult> GetAllRssFeeds()
        {
            // get all rss feeds from database
            var response = await rssFetchService.GetAllRssFeeds();

            // check if response is failure
            if (response.IsFailure)
            {
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteRssFeed(int id)
        {
            // Delete RSS feed by ID
            var response = await rssFetchService.DeleteRssFeed(id);

            // Check if response is failure
            if (response.IsFailure)
            {
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        }

    }
}