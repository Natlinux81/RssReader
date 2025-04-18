using Domain.Entities;
using Domain.Interface;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRoleRepository(RssReaderDbContext rssReaderDbContext) : IUserRoleRepository
{
    public async Task<bool> AddRoleAsync(int userId, int roleId)
    {
        try
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            await rssReaderDbContext.UserRoles.AddAsync(userRole);
            var result = await rssReaderDbContext.SaveChangesAsync() > 0;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> RemoveRoleAsync(int userId, int roleId)
    {
        try
        {
            var userRole =
                await rssReaderDbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId);
            if (userRole is null) return false;
            rssReaderDbContext.UserRoles.Remove(userRole);
            var result = await rssReaderDbContext.SaveChangesAsync() > 0;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> HasRoleAsync(int userId, int roleId)
    {
        try
        {
           var isUserHasRole = await rssReaderDbContext.UserRoles.
               AnyAsync(u => u.UserId == userId && u.RoleId == roleId);
           return isUserHasRole;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}