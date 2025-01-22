using SubscriptionManager.Models;
using SubscriptionManager.Models.Responses;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Services.Mappers;

public class UserMapper : IUserMapper
{
    public UserResponse Map(User user)
        => new()
        {
            Id = user.Id,
            UserName = user.Username
        };

    public List<UserResponse> Map(List<User> users) 
        => users.Select(user => Map(user)).ToList();
}