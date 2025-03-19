using SubscriptionManager.Models.Enums;

namespace SubscriptionManager.Models.Requests;

public class DiscountRequest
{
    public int SubscriptionId { get; set; }
    public DiscountType DiscountType { get; set; }
    public int Repetitions { get; set; }
}