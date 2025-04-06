using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(User user);
    Task<string> GenerateAndSaveRefreshTokenAsync(User user);
    Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
    Task<TokenResponse> CreateTokenResponse(User user);
}