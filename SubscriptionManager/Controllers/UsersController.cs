using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionManager.Models;
using SubscriptionManager.Models.Responses;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    [Route("/PersonalAccount")]
    public async Task<ActionResult<User>> GetPersonalAccount() 
        => await _userService.QueryPersonalAccount();

    [HttpGet]
    [Route("/{username}")]
    public async Task<ActionResult<UserResponse>> SearchUserProfile(string username)
        => await _userService.QueryUserProfile(username);
    
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers() 
        => await _userService.QueryAllUsers();
}