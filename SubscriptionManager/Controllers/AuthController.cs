using BasicAuth.Models;
using BasicAuth.Models.Requests;
using BasicAuth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost]
    [Route("/Register")]
    public async Task<ActionResult<User>> Register(RegisterRequest request)
        => await _userService.CreateUser(request);

    [HttpPost]
    [Route("/Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var jwt = await _authService.Login(request);
        
        Response.Cookies.Append("jwt", jwt, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Lax
        });

        return Ok(new
        {
            message = "success",
            jwt
        });
    }

    [HttpPost]
    [Route("/Logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            message = "success"
        });
    }
}