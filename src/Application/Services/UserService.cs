using Application.Common.Results;
using Application.DTOs;
using Application.Errors;
using Application.Interfaces;
using Domain.Interface;

namespace Application.Services;

public class UserService( IUnitOfWork unitOfWork,
    IUserRepository userRepository) : IUserService
{
    public async Task<Result> GetAllUsers()
    {
        var users = await userRepository.GetAllAsync();
        if (users.Count == 0) return Result.Failure(AuthError.UserNotFound);
        
        var userDtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            UserRoles = u.UserRoles.Select(ur => new UserRoleDto
            {
                Role = ur.Role,
            }).ToList(),
            Username = u.Username,
            Email = u.Email,
            Password = u.Password
        }).ToList();

        return Result.Success(userDtos);
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