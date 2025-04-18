using Domain.Entities;

namespace Domain.Interface;

public interface IUserRepository : IGenericRepository<User>
{
    // extra implementation
    Task<User?> GetUserByEmailAsync(string email);
    Task<List<string>> GetUserRolesByEmailAsync(string email);

    Task<User?> GetUserByIdAsync(Guid id);
}