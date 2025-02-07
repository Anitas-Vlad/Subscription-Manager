using SubscriptionManager.Models;

namespace SubscriptionManager.Services.Interfaces;

public interface IDiscountHandlingService
{
    void HandleNoDiscountPayment(Payment payment, Subscription subscription);
    void HandleFreeMonthDiscountPayment(Payment payment, Subscription subscription);
    void Handle50PercentOffMonthDiscountPayment(Payment payment, Subscription subscription);
    void HandleOneTimeFreeMonthDiscountPayment(Payment payment, Subscription subscription);
    void HandleOneTime50PercentOffMonthDiscountPayment(Payment payment, Subscription subscription);
}