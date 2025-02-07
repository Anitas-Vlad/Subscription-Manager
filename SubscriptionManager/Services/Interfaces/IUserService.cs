using SubscriptionManager.Models;
using SubscriptionManager.Models.Requests;
using SubscriptionManager.Models.Responses;

namespace SubscriptionManager.Services.Interfaces;

public interface IUserService
{
    Task<User> QueryUserById(int userId);
    Task<UserResponse> QueryUserProfile(string username);
    Task<User> QueryPersonalAccount();
    Task<List<UserResponse>> QueryAllUsers();
    Task<User?> QueryUserByEmail(string userEmail);
    Task<User> CreateUser(RegisterRequest request);
}