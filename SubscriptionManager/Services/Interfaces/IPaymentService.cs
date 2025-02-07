using SubscriptionManager.Models.Responses;

namespace SubscriptionManager.Services.Interfaces;

public interface IPaymentService
{
    Task<PaymentsResponse> GetPaymentsForThisMonth();
}