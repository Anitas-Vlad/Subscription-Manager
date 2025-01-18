using BasicAuth.Models.Requests;

namespace BasicAuth.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginRequest request);
}