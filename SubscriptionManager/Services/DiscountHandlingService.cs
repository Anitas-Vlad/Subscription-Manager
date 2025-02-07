using SubscriptionManager.Context;
using SubscriptionManager.Models;
using SubscriptionManager.Models.Enums;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Services;

public class DiscountHandlingService : IDiscountHandlingService
{
    private readonly SubscriptionManagerContext _context;

    public DiscountHandlingService(SubscriptionManagerContext context)
    {
        _context = context;
    }
    
    private void UpdateBasicPayment(Subscription subscription, Payment payment)
    {
        payment.SubscriptionId = subscription.Id;
        payment.Date = DateTime.Now;
        subscription.HandlePaymentDates();
    }
    
    public void HandleNoDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        payment.Amount = basePrice;
        payment.Discount = 0;
        UpdateBasicPayment(subscription, payment);

        _context.Subscriptions.Update(subscription);
        _context.Payments.Add(payment);
        _context.Discounts.Update(discount);
    }

    public void HandleFreeMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        discount.Repetitions -= 1;
        if (discount.Repetitions == 0) discount.Type = DiscountType.None;
        payment.Amount = 0;
        payment.Discount = basePrice;
        UpdateBasicPayment(subscription, payment);

        _context.Subscriptions.Update(subscription);
        _context.Payments.Add(payment);
        _context.Discounts.Update(discount);
    }

    public void Handle50PercentOffMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        if (discount.Repetitions == 0) discount.Type = DiscountType.None;
        payment.Amount = basePrice / 2;
        payment.Discount = basePrice / 2;
        UpdateBasicPayment(subscription, payment);

        _context.Subscriptions.Update(subscription);
        _context.Payments.Add(payment);
        _context.Discounts.Update(discount);
    }

    public void HandleOneTimeFreeMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        discount.Type = DiscountType.None;
        payment.Amount = basePrice/2;
        payment.Discount = basePrice/2;
        UpdateBasicPayment(subscription, payment);

        _context.Subscriptions.Update(subscription);
        _context.Payments.Add(payment);
        _context.Discounts.Update(discount);
    }
    
    public void HandleOneTime50PercentOffMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        discount.Type = DiscountType.None;
        payment.Amount = basePrice/2;
        payment.Discount = basePrice/2;
        UpdateBasicPayment(subscription, payment);

        _context.Subscriptions.Update(subscription);
        _context.Payments.Add(payment);
        _context.Discounts.Update(discount);
    }
}