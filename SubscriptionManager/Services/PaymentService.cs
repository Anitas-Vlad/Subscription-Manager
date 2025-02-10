using SubscriptionManager.Context;
using SubscriptionManager.Models;
using SubscriptionManager.Models.Enums;
using SubscriptionManager.Models.Responses;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Services;

public class PaymentService : IPaymentService
{
    private readonly IUserService _userService;
    private readonly IPaymentsMapper _paymentsMapper;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IDiscountHandlingService _discountHandlingService;
    private readonly SubscriptionManagerContext _context;

    public PaymentService(IUserService userService, IPaymentsMapper paymentsMapper,
        ISubscriptionService subscriptionService, SubscriptionManagerContext context,
        IDiscountHandlingService discountHandlingService)
    {
        _userService = userService;
        _paymentsMapper = paymentsMapper;
        _context = context;
        _discountHandlingService = discountHandlingService;
        // _subscriptionService = subscriptionService;
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

    public async Task<Payment> PaySubscription(Subscription subscription)
        //TODO check if it's better like this or find it with id
    {
        if (!subscription.Active)
            throw new ArgumentException("Subscription is not active.");

        var payment = new Payment();

        ApplyDiscount(subscription, payment);

        _context.Subscriptions.Update(subscription);
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return payment;
    }

    private void ApplyDiscount(Subscription subscription, Payment payment)
    {
        switch (subscription.Discount.Type) //TODO Break into smaller method.
        {
            case DiscountType.None:
                _discountHandlingService.HandleNoDiscountPayment(payment, subscription);
                break;

            case DiscountType.RecursiveFreeMonth:
                _discountHandlingService.HandleRecursiveFreeMonthDiscountPayment(payment, subscription);
                break;

            case DiscountType.RecursiveOff50PercentMonth:
                _discountHandlingService.HandleRecursive50PercentOffMonthDiscountPayment(payment, subscription);
                break;
            
            case DiscountType.RecursiveOff20PercentMonth: 
                _discountHandlingService.HandleRecursive20PercentOffMonthDiscountPayment(payment, subscription);
                break;

            case DiscountType.FreeOneTime:
                _discountHandlingService.HandleOneTimeFreeMonthDiscountPayment(payment, subscription);
                break;

            case DiscountType.OneTimeOff50Percent:
                _discountHandlingService.HandleOneTime50PercentOffMonthDiscountPayment(payment, subscription);
                break;

            case DiscountType.OneTimeOff20Percent: 
                _discountHandlingService.HandleOneTime20PercentOffMonthDiscountPayment(payment, subscription);
                break;
            
            
            case DiscountType.FreeYear: break;
            case DiscountType.Off50PercentYear: break;
            case DiscountType.FreeWeek: break;
            case DiscountType.Off50PercentWeek: break;
            default:
                throw new ArgumentOutOfRangeException("Unknown discount type.");
        }
    }
}