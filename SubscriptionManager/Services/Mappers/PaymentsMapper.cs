using SubscriptionManager.Models;
using SubscriptionManager.Models.Responses;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Services.Mappers;

public class PaymentsMapper : IPaymentsMapper
{
    public PaymentsResponse MapPaymentResponse(List<Payment> payments)
    {
        var paymentResponse = new PaymentsResponse()
        {
            Payments = payments,
            Amount = payments.Sum(payment => payment.Amount)
        };

        return paymentResponse;
    }
}