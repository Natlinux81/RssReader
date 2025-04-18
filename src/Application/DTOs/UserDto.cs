namespace Application.DTOs;

public record UserDto(
    int Id, string Email, 
    string Username, 
    List<string> Roles);