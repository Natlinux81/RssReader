using Application.Common.Results;
using Application.DTOs;
using Application.Models;

namespace Application.Interfaces;

public interface IUserService
{
    Task<Result<PagedResult<UserDto>>> GetAsync(int pageNumber = 1, int pageSize = 10);
    Task<Result<string>> UpdateAsync(UserUpdateRequest user);
    Task<Result<string>> DeleteAsync(int id );
    Task<Result<UserDto>> GetUserByIdAsync(int id);
    Task<Result<string>> AssingRoleAsync(AssingRoleRequest roleRequest);
    Task<Result<string>> RevokeRoleAsync(AssingRoleRequest roleRequest);
}