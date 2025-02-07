using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SubscriptionManager.Context;
using SubscriptionManager.Models;
using SubscriptionManager.Models.Requests;
using SubscriptionManager.Models.Responses;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Services;

public class UserService : IUserService
{
    private readonly SubscriptionManagerContext _context;
    private static Regex _mailPattern;
    private static Regex _passwordPattern;
    private readonly IUserContextService _userContextService;
    private readonly IUserMapper _userMapper;

    public UserService(SubscriptionManagerContext context, IUserContextService userContextService,
        IUserMapper userMapper)
    {
        _context = context;
        _mailPattern = new("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
        _passwordPattern = new("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
        _userContextService = userContextService;
        _userMapper = userMapper;
    }

    public async Task<User> QueryUserById(int userId)
    {
        var user = await _context.Users
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null) throw new ArgumentException("User not found.");

        return user;
    }

    private async Task<User> QueryUserByUsername(string username)
    {
        var user = await _context.Users
            .Where(user => user.Username == username)
            .FirstOrDefaultAsync();

        if (user == null)
            throw new ArgumentException("User not found.");

        return user;
    }

    public async Task<UserResponse> QueryUserProfile(string username)
    {
        var user = await QueryUserByUsername(username);
        var userResponse = _userMapper.Map(user);
        return userResponse;
    }

    public async Task<User> QueryPersonalAccount()
    {
        var userId = _userContextService.GetUserId();

        var user = await _context.Users
            .Include(user => user.Subscriptions)
            .Where(user => user.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null) throw new ArgumentException("User not found.");

        return user;
    }

    public async Task<List<UserResponse>> QueryAllUsers() 
        => _userMapper.Map(await _context.Users.ToListAsync());

    public async Task<User?> QueryUserByEmail(string userEmail)
        => await _context.Users
            .Where(user => user.Email == userEmail)
            .FirstOrDefaultAsync();

    public async Task<User> CreateUser(RegisterRequest request)
    {
        await IsEmailValid(request.Email);
        await IsUsernameValid(request.Username);
        IsPasswordValid(request.Password);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            Email = request.Email
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private async Task IsEmailValid(string userEmail)
    {
        if (!_mailPattern.IsMatch(userEmail))
            throw new ArgumentException("Please enter a valid email.");

        if (await _context.Users.AnyAsync(user => user.Email == userEmail))
            throw new ArgumentException($"The email: \"{userEmail}\" in use.");
    }

    private async Task IsUsernameValid(string username)
    {
        if (await _context.Users.AnyAsync(user => user.Username == username))
            throw new ArgumentException($"the username \"{username}\" is taken.");
    }

    private static void IsPasswordValid(string userPassword)
    {
        if (!_passwordPattern.IsMatch(userPassword))
            throw new ArgumentException(
                "Password must contain special characters, numbers, capital letters and be longer than 8 characters.");
    }
}