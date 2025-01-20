using BasicAuth.Models;
using SubscriptionManager.Models;

namespace BasicAuth.Services.Interfaces;

public interface IJwtService
{
    string CreateToken(User user);
}