namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public int RssFeedId { get; set; }
    public RssFeed? RssFeed { get; set; }
    public List<UserRole> UserRoles { get; set; }
    public List<RssFeed> RssFeeds { get; set; }
}