using System.ComponentModel.DataAnnotations;

namespace SubscriptionManager.Models.Responses;

public class PaymentsResponse
{
    [Required] public List<Payment> Payments { get; set; }
    [Required] public double Amount { get; set; }
}