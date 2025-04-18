namespace Application.Models;

public record TokenResponse(
    string? AccessToken,
    string? RefreshToken);