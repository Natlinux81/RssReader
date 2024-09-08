using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RssFeedController : BaseApiController
    {
        public readonly RssFetchService RssFetchService;
        
        public RssFeedController(RssFetchService fetchService)
        {
            this.RssFetchService = fetchService;
        }

         [HttpGet]
        public async Task<IActionResult> GetRssFeed([FromQuery] string feedUrl = "https://www.tagesschau.de/wirtschaft/technologie/index~rss2.xml")
        {
            if (string.IsNullOrEmpty(feedUrl))
                return BadRequest("Feed URL cannot be empty.");

            try
            {
                return Ok(await this.RssFetchService.Fetch(this.HttpContext.RequestAborted, new Uri(feedUrl)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving the RSS feed: {ex.Message}");
            }
        }
    }
}