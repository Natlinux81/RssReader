using Application.Common.Results;
using Application.Error;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services;

public class AuthenticationService (
    IUnitOfWork unitOfWork,
    IUserRepository iUserRepository) : IAuthenticationService
{
    public async Task<Result> RegisterAsync(RegisterRequest registerRequest)
    {
        if (registerRequest == null)
        {
            throw new ArgumentNullException(nameof(registerRequest));
        }

        var userExists = await iUserRepository.GetByEmailAsync(registerRequest.Email);
        if (userExists is not null) return Result.Failure(RssFeedError.RssFeedAlreadyExists);

        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            Password = registerRequest.Password,
        };
        await iUserRepository.AddAsync(user);
        await unitOfWork.CommitAsync();
        return Result.Success("User registered successfully.");
    }

    public Task<Result> LoginAsync(LoginRequest loginRequest)
    {
        throw new NotImplementedException();
    }
}