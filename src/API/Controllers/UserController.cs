using API.Extension;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController(IUserService userService) : BaseApiController
{
    [Authorize]
    [HttpGet]
    public async Task<IResult> GetAllUsers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var users = await userService.GetAsync(pageNumber, pageSize);
        return users.ToHttpResponse();
    }

    [HttpPut]
    public async Task<IResult> UpdateUser([FromBody] UserUpdateRequest userUpdateRequest)
    {
        var result = await userService.UpdateAsync(userUpdateRequest);
        return result.ToHttpResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteUser(int id)
    {
        var result = await userService.DeleteAsync(id);
        return result.ToHttpResponse();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetUserById(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        return user.ToHttpResponse();
    }
    [HttpPost("assign-role")]
    public async Task<IResult> AssignRole([FromBody] AssingRoleRequest roleRequest)
    
    {
        var result = await userService.AssingRoleAsync(roleRequest);
        return result.ToHttpResponse();
    }
    [HttpDelete("revoke-role")]
    public async Task<IResult> RevokeRole([FromBody] AssingRoleRequest roleRequest)
    {
        var result = await userService.RevokeRoleAsync(roleRequest);
        return result.ToHttpResponse();
    }
}