using Domain.Entities;

namespace Application.DTOs;

public class UserRoleDto
{
    public int UserId { get; set; }
    public User? User { get; set; }

    public int RoleId { get; set; }
    public Role? Role { get; set; }
}