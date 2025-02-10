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

    private static void UpdateBasePayment(Subscription subscription, Payment payment)
    {
        payment.SubscriptionId = subscription.Id;
        payment.Date = DateTime.Now;
        subscription.HandlePaymentDates();
    }

    public void HandleNoDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        payment.Discount = 0;
        payment.Amount = basePrice;
        UpdateBasePayment(subscription, payment);

        _context.Discounts.Update(discount);
    }

    public void HandleRecursiveFreeMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        discount.Repetitions -= 1;
        if (discount.Repetitions == 0) discount.Type = DiscountType.None;
        payment.Discount = basePrice;
        payment.Amount = 0;
        UpdateBasePayment(subscription, payment);

        _context.Discounts.Update(discount);
    }

    public void HandleRecursive50PercentOffMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        if (discount.Repetitions == 0) discount.Type = DiscountType.None;
        payment.Discount = basePrice / 2;
        payment.Amount = basePrice - payment.Discount;
        UpdateBasePayment(subscription, payment);

        _context.Discounts.Update(discount);
    }

    public void HandleRecursive20PercentOffMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        if (discount.Repetitions == 0) discount.Type = DiscountType.None;
        payment.Discount = basePrice / 5;
        payment.Amount = basePrice - payment.Discount;
        UpdateBasePayment(subscription, payment);

        _context.Discounts.Update(discount);
    }

    public void HandleOneTimeFreeMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        discount.Type = DiscountType.None;
        payment.Discount = basePrice / 2;
        payment.Amount = basePrice - payment.Discount;
        UpdateBasePayment(subscription, payment);

        _context.Discounts.Update(discount);
    }

    public void HandleOneTime50PercentOffMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        discount.Type = DiscountType.None;
        payment.Discount = basePrice / 2;
        payment.Amount = basePrice - payment.Discount;
        UpdateBasePayment(subscription, payment);

        _context.Discounts.Update(discount);
    }

    public void HandleOneTime20PercentOffMonthDiscountPayment(Payment payment, Subscription subscription)
    {
        var basePrice = subscription.Price;
        var discount = subscription.Discount;

        discount.Type = DiscountType.None;
        payment.Discount = basePrice / 5;
        payment.Amount = basePrice - payment.Discount;
        UpdateBasePayment(subscription, payment);

        _context.Discounts.Update(discount);
    }
}