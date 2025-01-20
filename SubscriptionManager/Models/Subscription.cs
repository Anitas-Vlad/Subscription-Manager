using TimeSpan = SubscriptionManager.Models.Enums.TimeSpan;

namespace SubscriptionManager.Models;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public DateTime PayTime { get; set; }

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