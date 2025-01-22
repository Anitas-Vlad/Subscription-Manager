using System.ComponentModel.DataAnnotations;
using TimeSpan = SubscriptionManager.Models.Enums.TimeSpan;

namespace SubscriptionManager.Models;

public class Subscription
{
    public int Id { get; set; }
    [Required] public int UserId { get; set; }
    [Required] public string Name { get; set; }
    [Required] public double Price { get; set; }
    [Required] public bool Active { get; set; } = true;
    [Required] public TimeSpan TimeSpan { get; set; }
    [Required] private DateTime NextPayTime { get; set; }
    private DateTime LastPayTime { get; set; }

    private void UpdateNextPayTime()
    {
        NextPayTime = TimeSpan switch
        {
            TimeSpan.Weekly => NextPayTime.AddDays(7),
            TimeSpan.Monthly => NextPayTime.AddMonths(1),
            _ => NextPayTime
        };
    }

    public void HandlePayment()
    {
        LastPayTime = DateTime.Now;
        UpdateNextPayTime();
    }
}