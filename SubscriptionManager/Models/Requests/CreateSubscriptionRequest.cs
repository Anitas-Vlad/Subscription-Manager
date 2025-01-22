using System.ComponentModel.DataAnnotations;
using TimeSpan = SubscriptionManager.Models.Enums.TimeSpan;

namespace SubscriptionManager.Models.Requests;

public class CreateSubscriptionRequest
{
    [Required] public string Name { get; set; }
    [Required] public double Price { get; set; }
    public int UserId { get; set; }
    [Required] public TimeSpan TimeSpan { get; set; }
}