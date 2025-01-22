using SubscriptionManager.Models;

namespace SubscriptionManager.Services.Interfaces;

public interface IJwtService
{
    string CreateToken(User user);
}