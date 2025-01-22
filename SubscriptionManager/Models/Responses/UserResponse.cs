using System.ComponentModel.DataAnnotations;

namespace SubscriptionManager.Models.Responses;

public class UserResponse
{
    public int Id { get; set; }
    [Required] public string UserName { get; set; }
}