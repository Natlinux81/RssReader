using Domain.Entities;

namespace Domain.Interface;

public interface IUserRepository : IGenericRepository<User>
{
    // extra implementation
    Task<User?> GetByEmailAsync(string email);
    Task<List<string>> GetUserRolesByEmailAsync(string email);
}