using API.Extension;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController (IUserService userService) : BaseApiController
{
    [Authorize (Roles = "Admin")]
    [HttpGet("admin-only")]
    public async Task<IResult> GetAllUsers()
    {
        var response = await userService.GetAllUsers();
        return response.ToHttpResponse();
    }
}