using System.ComponentModel.DataAnnotations;

namespace SubscriptionManager.Models;

public class User
{
    public int Id { get; set; }
    [Required] public string Username { get; set; }
    [Required] public string PasswordHash { get; set; }
    [Required] public string Email { get; set; }
    [Required] public List<Subscription> Subscriptions { get; set; } = new();
    [Required] public double PaidAmount { get; set; } = 0;
    
    public void AddToPaidAmount(double paidAmount) => PaidAmount += paidAmount;

    public void PaySubscription(int subscriptionId)
    {
        var subscription = Subscriptions.SingleOrDefault(sub => sub.Id == subscriptionId);
        
        
    }
}