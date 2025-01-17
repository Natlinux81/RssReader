using Application.Common.Results;
using Application.Errors;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services;

public class AuthenticationService (
    IUnitOfWork unitOfWork,
    IUserRepository iUserRepository) : IAuthenticationService
{
    public async Task<Result> RegisterAsync(RegisterRequest? registerRequest)
    {
        if (registerRequest is null)
        {
           return Result.Failure(AuthError.InvalidRegisterRequest);
        }

        var userExists = await iUserRepository.GetByEmailAsync(registerRequest.Email);
        if (userExists is not null) return Result.Failure(AuthError.UserAlreadyExists);

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

    public async Task<Result> LoginAsync(LoginRequest? loginRequest)
    {
        if (loginRequest is null)
        {
            return Result.Failure(AuthError.InvalidLoginRequest);
        }

        var (email, password) = (loginRequest);
        var user = await iUserRepository.GetByEmailAsync(email);
        if (user is null) return Result.Failure(AuthError.UserNotFound);
        if (user.Password != password)
        {
            return Result.Failure(AuthError.InvalidPassword);   
        }
        var token = "token";
        var result = new
        {
            Token = token,
            Username = user.Username
        };
        return Result.Success(result);
    }

}