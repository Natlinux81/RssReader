using Application.Common.Results;

namespace Application.Interfaces;

public interface IUserService
{
    Task<Result> GetAllUsers();
    Task<Result> DeleteUser(int id);
    Task<Result> UpdateUser(int id);
}