using API.Extension;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController(IAuthenticationService authService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<IResult> Register(RegisterRequest registerRequest)
    {
        var response = await authService.RegisterAsync(registerRequest);
        return response.ToHttpResponse();
    }

    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequest loginRrequest)
    {
        return Results.Ok();
    }
}


