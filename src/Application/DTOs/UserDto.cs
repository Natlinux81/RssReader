namespace Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    
    public List<UserRoleDto> UserRoles { get; set; } = [];
    public List <RssFeedDto> RssFeeds { get; set; } = [];
}