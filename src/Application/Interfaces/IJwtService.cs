namespace Application.Interfaces;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(string email);
}