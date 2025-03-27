using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(User user);
}