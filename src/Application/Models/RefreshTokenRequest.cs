namespace Application.Models;

public record RefreshTokenRequest(
    Guid UserId,
    string RefreshToken);