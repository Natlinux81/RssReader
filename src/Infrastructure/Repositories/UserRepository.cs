using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(RssReaderDbContext rssReaderDbContext)
    : GenericRepository<User>(rssReaderDbContext), IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await rssReaderDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<string>> GetUserRolesByEmailAsync(string email)
    {
        return await rssReaderDbContext.Users
            .Where(ur => ur.Email == email)
            .SelectMany(ur => ur.UserRoles)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await rssReaderDbContext.Users.FindAsync(id);
    }
}