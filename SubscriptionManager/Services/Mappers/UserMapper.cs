using BasicAuth.Models;
using BasicAuth.Models.Responses;
using BasicAuth.Services.Interfaces;

namespace BasicAuth.Services.Mappers;

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