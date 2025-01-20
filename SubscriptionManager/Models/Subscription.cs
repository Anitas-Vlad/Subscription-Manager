using System.ComponentModel.DataAnnotations;
using TimeSpan = SubscriptionManager.Models.Enums.TimeSpan;

namespace SubscriptionManager.Models;

public class Subscription
{
    public int Id { get; set; }
    [Required] public int UserId { get; set; }
    [Required] public string Name { get; set; }
    [Required] public double Price { get; set; }
    [Required] public TimeSpan TimeSpan { get; set; }
    [Required] private DateTime PayTime { get; set; }

    public void UpdateNextPayTime()
    {
        PayTime = TimeSpan switch
        {
            TimeSpan.Weekly => PayTime.AddDays(7),
            TimeSpan.Monthly => PayTime.AddMonths(1),
            _ => PayTime
        };
    }
}