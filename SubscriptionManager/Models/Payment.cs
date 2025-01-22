namespace SubscriptionManager.Models;

public class Payment
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public int UserId { get; set; }
    public DateTime PaymentDate { get; set; }
}