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
            var rssFeed = await rssFetchService.RssFeedGet(CancellationToken, new Uri(feedUrl));
            return Results.Ok(rssFeed);
        }

        [HttpPost("rssfeed")]
        public async Task<IResult> AddRssFeed(RssFeedRequest rssFedRequest)  
        {
            var response = await rssFetchService.RssFeedAdd(rssFedRequest);
            return Results.Ok(response);
        }
    }
}