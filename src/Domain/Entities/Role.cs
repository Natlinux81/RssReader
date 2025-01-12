namespace Domain.Entities;

public class Role
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public List<UserRole> UserRoles { get; set; } = [];
}