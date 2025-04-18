using Application.Common.Results;
using Application.DTOs;
using Application.Errors;
using Application.Interfaces;
using Application.Models;
using Application.Validators;
using Domain.Interface;

namespace Application.Services;

public class UserService(
    IUnitOfWork unitOfWork,
    UserUpdateRequestValidator userUpdateRequestValidator,
    IUserRepository userRepository,
    IUserRoleRepository userRoleRepository) : IUserService
{
    public async Task<Result<PagedResult<UserDto>>> GetAsync(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            // fetch all users
            var users = await userRepository.GetAllAsync();
            var totalCount = users.Count();
            var pagedItems = users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserDto(
                    user.Id,
                    user.Email,
                    user.Username,
                    user.UserRoles.Select(x => x.Role.Name).ToList()
                ))
                .ToList();
            
            var pageResult = new PagedResult<UserDto>
            {
                Items = pagedItems,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.Success(pageResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<string>> UpdateAsync(UserUpdateRequest userUpdateRequest)
    {
        try
        {
            // validate request
            var validationResult = await userUpdateRequestValidator.ValidateAsync(userUpdateRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(a => a.ErrorMessage);
                return Result.Failure<string>(UserError.CreateInvalidUserUpdateRequestError(errors));
            }
            
            // check if user exists
            var user = await userRepository.GetByIdAsync(userUpdateRequest.Id);
            if (user == null)
            {
                return Result.Failure<string>(UserError.UserNotFound); 
            }

            // update user
            user.Username = userUpdateRequest.Username;
            user.Email = userUpdateRequest.Email;
            userRepository.Update(user);
            await unitOfWork.CommitAsync();
            return Result.Success("User updated successfully");     
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<string>> DeleteAsync(int id)
    {
        try
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return Result.Failure<string>(UserError.UserNotFound);
            }

            userRepository.Delete(user);
            await unitOfWork.CommitAsync();
            return Result.Success("User deleted successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<UserDto>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user is null)
            {
                return Result.Failure<UserDto>(UserError.UserNotFound);
            }
            var userDetails = new UserDto(
                user.Id,
                user.Email,
                user.Username,
                user.UserRoles.Select(x => x.Role.Name).ToList()
            );
            return Result.Success(userDetails);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<string>> AssingRoleAsync(AssingRoleRequest roleRequest)
    {
        try
        {
            var isUserHasRole = userRoleRepository.HasRoleAsync(roleRequest.UserId, roleRequest.RoleId);
            if (isUserHasRole.Result)
            {
                return Result.Failure<string>(UserError.UserAlreadyHasRole);
            }
            var result = await userRoleRepository.AddRoleAsync(roleRequest.UserId, roleRequest.RoleId);
            return result ? Result.Success("Role assigned successfully") : Result.Failure<string>(UserError.FailedToAssignRole);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<string>> RevokeRoleAsync(AssingRoleRequest roleRequest)
    {
        try
        {
            var isUserHasRole = userRoleRepository.HasRoleAsync(roleRequest.UserId, roleRequest.RoleId);
            if (!isUserHasRole.Result)
            {
                return Result.Failure<string>(UserError.UserHasNoRole);
            }

            var result = await userRoleRepository.RemoveRoleAsync(roleRequest.UserId, roleRequest.RoleId);
            return result
                ? Result.Success("Role revoked successfully")
                : Result.Failure<string>(UserError.FailedToRevokeRole);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
