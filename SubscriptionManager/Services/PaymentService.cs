using SubscriptionManager.Models;
using SubscriptionManager.Models.Responses;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Services;

public class PaymentService : IPaymentService
{
    private readonly IUserService _userService;
    private readonly IPaymentsMapper _paymentsMapper;

    public PaymentService(IUserService userService, IPaymentsMapper paymentsMapper)
    {
        _userService = userService;
        _paymentsMapper = paymentsMapper;
    }
    
    public async Task<PaymentsResponse> GetPaymentsForThisMonth()
    {
        var user = await _userService.QueryPersonalAccount();
        var subscriptions = user.Subscriptions;

        var paymentsThisMonth = new List<Payment>();

        foreach (var subscription in subscriptions)
        {
            paymentsThisMonth.AddRange(subscription.Payments.Where(payment =>
                payment.Date.Month == DateTime.Now.Date.Month &&
                payment.Date.Year == DateTime.Now.Date.Year));
        }

        return _paymentsMapper.MapPaymentResponse(paymentsThisMonth);
    }
}