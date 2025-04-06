using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RoleId });
        builder.HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId);
        
        builder.HasData(
            new UserRole
            {
                UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                RoleId = 1 
            },
            new UserRole
            {
                UserId = Guid.Parse("22222222-2222-2222-2222-222222222222"), 
                RoleId = 2 
            }
        );
    }
    
}