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
    public Discount Discount { get; set; }
    [Required] public TimeSpan TimeSpan { get; set; }
    public DateTime LastPayTime { get; set; }
    [Required] public DateTime NextPayTime { get; set; }

    [Required] public List<Payment> Payments { get; set; } = new();

    private void UpdateNextPayTime()
    {
        NextPayTime = TimeSpan switch
        {
            TimeSpan.Weekly => NextPayTime.AddDays(7),
            TimeSpan.Monthly => NextPayTime.AddMonths(1),
            _ => NextPayTime
        };
    }

    public void ApplyDiscount(Discount discount) 
        => Discount = discount;

    public void HandlePaymentDates() //TODO 
    {
        LastPayTime = DateTime.Now;
        UpdateNextPayTime();
    }

    public List<Payment> GetPaymentsForThisMonth()
        => Payments.Where(payment => payment.Date.Month == DateTime.Now.Month).ToList();

    public void CancelSubscription() 
        => Active = false;
}