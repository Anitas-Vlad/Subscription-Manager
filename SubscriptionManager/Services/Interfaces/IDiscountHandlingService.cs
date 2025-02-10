using SubscriptionManager.Models;

namespace SubscriptionManager.Services.Interfaces;

public interface IDiscountHandlingService
{
    void HandleNoDiscountPayment(Payment payment, Subscription subscription);
    void HandleRecursiveFreeMonthDiscountPayment(Payment payment, Subscription subscription);
    void HandleRecursive50PercentOffMonthDiscountPayment(Payment payment, Subscription subscription);
    void HandleRecursive20PercentOffMonthDiscountPayment(Payment payment, Subscription subscription);
    void HandleOneTimeFreeMonthDiscountPayment(Payment payment, Subscription subscription);
    void HandleOneTime50PercentOffMonthDiscountPayment(Payment payment, Subscription subscription);
    void HandleOneTime20PercentOffMonthDiscountPayment(Payment payment, Subscription subscription);
}