using Application.Common.Results;
using Application.Errors;
using Application.Interfaces;
using Application.Models;
using Application.Validators;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Utilities;

namespace Application.Services;

public class AuthenticationService (
    IUnitOfWork unitOfWork,
    IUserRepository iUserRepository,
    LoginRequestValidator loginRequestValidator,
    RegisterRequestValidator registerRequestValidator,
    IJwtService jwtService) : IAuthenticationService
{
    public async Task<Result> RegisterAsync(RegisterRequest registerRequest)
    {
        var validationResult = await registerRequestValidator.ValidateAsync(registerRequest);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return Result.Failure(AuthError.CreateInvalidRegisterRequestError(errors));
        } 

        var userExists = await iUserRepository.GetUserByEmailAsync(registerRequest.Email);
        if (userExists is not null) return Result.Failure(AuthError.UserAlreadyExists);

        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            Password = PasswordHasher.HashPassword(registerRequest.Password)
        };
        await iUserRepository.AddAsync(user);
        await unitOfWork.CommitAsync();
        return Result.Success("User registered successfully.");
    }

    public async Task<Result> LoginAsync(LoginRequest loginRequest)
    {
        var validationResult = await loginRequestValidator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid) 
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return Result.Failure(AuthError.CreateInvalidLoginRequestError(errors));
        } 

        var (email, password) = loginRequest;
        var user = await iUserRepository.GetUserByEmailAsync(email);
        if (user is null) return Result.Failure(AuthError.UserNotFound);
        if (user.Password != password)
        {
            return Result.Failure(AuthError.InvalidPassword);   
        }
        var token = await jwtService.GenerateTokenAsync(user);
        var result = new
        {
            Token = token,
            Username = user.Username
        };
        return Result.Success(result);
    }

}