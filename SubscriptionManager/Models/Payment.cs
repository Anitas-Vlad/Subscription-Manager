namespace SubscriptionManager.Models;

public class Payment
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public string SubscriptionName { get; set; }
    public int UserId { get; set; }
    public double Amount { get; set; }
    public double Discount { get; set; }
    public DateTime Date { get; set; }
}