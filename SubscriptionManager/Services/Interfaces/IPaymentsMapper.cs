using SubscriptionManager.Models;
using SubscriptionManager.Models.Responses;

namespace SubscriptionManager.Services.Interfaces;

public interface IPaymentsMapper
{
    PaymentsResponse MapPaymentResponse(List<Payment> payments);
}