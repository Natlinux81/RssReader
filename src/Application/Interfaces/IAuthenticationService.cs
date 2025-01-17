using Application.Common.Results;
using Application.Models;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    Task<Result> RegisterAsync(RegisterRequest? request);
    Task<Result> LoginAsync(LoginRequest? request);
}