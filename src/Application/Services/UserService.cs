using Application.Common.Results;
using Application.DTOs;
using Application.Errors;
using Application.Interfaces;
using Application.Models;
using Domain.Interface;

namespace Application.Services;

public class UserService( IUnitOfWork unitOfWork,
    IUserRepository userRepository) : IUserService
{
    public async Task<Result> GetAllUsers()
    {
        var users = await userRepository.GetAllAsync();
        if (users.Count == 0) return Result.Failure(AuthError.UserNotFound);
        
        // var userDtos = users.Select(u => new RegisterRequest(u.Username, u.Email, u.PasswordHash)
        // {
        //     Username = u.Username,
        //     Email = u.Email,
        //     Password = u.PasswordHash
        // }).ToList();

        return Result.Success(users);
    }
    
    public Task<Result> DeleteUser(int id)
    {
      throw new NotImplementedException();
    }

    public Task<Result> UpdateUser(int id)
    {
       throw new NotImplementedException();
    }
}