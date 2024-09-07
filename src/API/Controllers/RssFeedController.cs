using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RssFeedController : BaseApiController
    {
        private readonly HttpClient _httpClient;

        public RssFeedController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

         [HttpGet]
        public async Task<IActionResult> GetRssFeed([FromQuery] string feedUrl = "https://www.tagesschau.de/wirtschaft/technologie/index~rss2.xml")
        {
            if (string.IsNullOrEmpty(feedUrl))
            {
                return BadRequest("Feed URL cannot be empty.");
            }

            try
            {
                // RSS-Feed call
                var response = await _httpClient.GetAsync(feedUrl);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        SyndicationFeed feed = SyndicationFeed.Load(reader);

                        var rssItems = new
                        {
                            Title = feed.Title.Text,
                            Items = feed.Items.Select(item => new
                            {
                                Title = item.Title.Text,
                                Description = item.Summary?.Text,
                                Link = item.Links[0].Uri.ToString(),
                                PublishDate = item.PublishDate.ToString("yyyy-MM-dd HH:mm"),
                                Image = item.Links.FirstOrDefault(link => link.MediaType?.StartsWith("image") == true)?.Uri.ToString()
                                
                            }).ToList()
                        };

                        return Ok(rssItems); // return RSS-Feed items im JSON-Format
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving the RSS feed: {ex.Message}");
            }
        }
    }
}