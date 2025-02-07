using System.ComponentModel.DataAnnotations;

namespace SubscriptionManager.Models;

public class User
{
    public int Id { get; set; }
    [Required] public string Username { get; set; }
    [Required] public string PasswordHash { get; set; }
    [Required] public string Email { get; set; }
    [Required] public List<Subscription> Subscriptions { get; set; } = new();

    public void PaySubscription(Subscription subscription) // TODO Create/Add Payment
        => throw new NotImplementedException();

    public Subscription? GetSubscriptionId(int subscriptionId)
        => Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);

    public void AddSubscription(Subscription subscription)
    {
        subscription.UserId = Id;
        Subscriptions.Add(subscription);
    }

    public void CancelSubscription(Subscription subscription)
    {
        subscription.CancelSubscription();
    }
}