using SubscriptionManager.Models.Requests;

namespace SubscriptionManager.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginRequest request);
}