namespace Application.Models;

public record RegisterRequest(
    string Username,
    string Email,
    string Password);