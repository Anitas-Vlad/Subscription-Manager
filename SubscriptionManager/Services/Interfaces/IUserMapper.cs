using BasicAuth.Models;
using BasicAuth.Models.Responses;
using SubscriptionManager.Models;

namespace BasicAuth.Services.Interfaces;

public interface IUserMapper
{
    UserResponse Map(User user);
    List<UserResponse> Map(List<User> users);
}