namespace Domain.Interface;

public interface IUserRoleRepository
{
    Task<bool> AddRoleAsync(int userId, int roleId);
    Task<bool> RemoveRoleAsync(int userId, int roleId);
    Task<bool> HasRoleAsync(int userId, int roleId);
}