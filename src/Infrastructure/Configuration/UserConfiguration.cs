using Domain.Entities;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(k => k.Id);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnType("varchar(250)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .HasAnnotation("RegularExpression", @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        builder.HasMany(x => x.UserRoles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasData(
            new User 
            {
                Id = 1,
                Username = "admin , user",
                PasswordHash = PasswordHasher.HashPassword("Admin@123"),
                Email = "admin@localhost.de"
            },
            new User
            {
                Id = 2, Username = "user",
                PasswordHash = PasswordHasher.HashPassword("User@123"),
                Email = "user@localhost.de"
            });
    }
}