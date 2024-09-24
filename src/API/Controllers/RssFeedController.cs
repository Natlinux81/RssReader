using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RssFeedController(IRssFetchService rssFetchService) : BaseApiController
    {         
        [HttpPost("api/[controller]")]
        public async Task<IResult> AddRssFeed(CancellationToken cancellationToken, RssFeedRequest rssFedRequest, string feedUrl)  
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
    }
}