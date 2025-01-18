using BasicAuth.Models;
using BasicAuth.Models.Requests;
using BasicAuth.Models.Responses;

namespace BasicAuth.Services.Interfaces;

public interface IUserService
{
    Task<User> QueryUserById(int userId);
    Task<UserResponse> QueryUserProfile(string username);
    Task<User> QueryPersonalAccount();
    Task<List<User>> QueryAllUsers();
    Task<User?> QueryUserByEmail(string userEmail);
    Task<User> CreateUser(RegisterRequest request);
}