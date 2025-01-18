using BasicAuth.Models.Requests;
using BasicAuth.Services.Interfaces;

namespace BasicAuth.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthService(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<string> Login(LoginRequest request)
    {
        var userFromDb = await _userService.QueryUserByEmail(request.Email);
        
        if (userFromDb == null || !BCrypt.Net.BCrypt.Verify(request.Password, userFromDb.PasswordHash)) 
            throw new ArgumentException("Incorrect credentials.");

        var token = _jwtService.CreateToken(userFromDb);

        return token;
    }
}