using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : BaseApiController
{
    [Authorize]
    [HttpGet]
    public string[] GetUsers()
    {
        return new[]
        {
            "User1", "User2", "User3", "User4", "User5"
        };
    }
}