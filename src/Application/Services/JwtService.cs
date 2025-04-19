using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService(
    IConfiguration configuration,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IJwtService
{
    public async Task<string> GenerateTokenAsync(User user)
    {
        var secretKey = configuration["Jwt:Key"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var roles = await userRepository.GetUserRolesByEmailAsync(user.Email);
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new("UserId", user.Id.ToString())
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1), // set a token expiration time
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    public async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(5);
        userRepository.Update(user);
        await unitOfWork.CommitAsync();
        return refreshToken;
    }

    public async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if (user is null || user.RefreshToken != refreshToken
                         || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return null;
        return user;
    }

    public async Task<TokenResponse> CreateTokenResponse(User user)
    {
        var accessToken = await GenerateTokenAsync(user);
        var refreshToken = await GenerateAndSaveRefreshTokenAsync(user);
        var result = new TokenResponse(accessToken, refreshToken);
        return result;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}