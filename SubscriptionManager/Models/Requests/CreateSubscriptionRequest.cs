using TimeSpan = SubscriptionManager.Models.Enums.TimeSpan;

namespace SubscriptionManager.Models.Requests;

public class CreateSubscriptionRequest
{
    public string Name { get; set; }
    public double Price { get; set; }
    public TimeSpan TimeSpan { get; set; }
}