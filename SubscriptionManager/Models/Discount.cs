using SubscriptionManager.Models.Enums;

namespace SubscriptionManager.Models;

public class Discount
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public bool Active { get; set; } = true;
    public DiscountType Type { get; set; }
    public int Repetitions { get; set; }
}