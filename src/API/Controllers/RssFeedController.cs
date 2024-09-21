using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RssFeedController(IRssFetchService rssFetchService) : BaseApiController
    {        
        [HttpGet]
        public async Task<IResult> GetRssFeed([FromQuery] CancellationToken CancellationToken, string feedUrl = "https://www.tagesschau.de/wirtschaft/technologie/index~rss2.xml")
        {
            // retrieve rss feed from url
            var rssFeed = await rssFetchService.RssFeedGet(CancellationToken, new Uri(feedUrl));
            return Results.Ok(rssFeed);
        }

        [HttpPost("rssfeed")]
        public async Task<IResult> AddRssFeed(RssFeedRequest rssFedRequest)  
        {
            // add rss feed to database
            var response = await rssFetchService.RssFeedAdd(rssFedRequest);
            
            // check if response is failure
            if (response.IsFailure)
            {
                return Results.BadRequest(response);
            }
            return Results.Ok(response);
        }
    }
}