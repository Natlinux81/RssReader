namespace Application.Models
{
    public record RssFeedItemRequest (string Title, string Link, string Description, DateTime PublishDate, string ImageUrl);
  
}