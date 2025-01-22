using SubscriptionManager.Models;
using SubscriptionManager.Models.Responses;

namespace SubscriptionManager.Services.Interfaces;

public interface IUserMapper
{
    UserResponse Map(User user);
    List<UserResponse> Map(List<User> users);
}