using SubscriptionManager.Models;
using SubscriptionManager.Models.Requests;

namespace SubscriptionManager.Services.Interfaces;

public interface IUserSubscriptionsService
{
    Task CancelSubscription(int subscriptionId);
    Task ApplyDiscountOnSubscription(DiscountRequest request);
    Task<Subscription> AddSubscription(CreateSubscriptionRequest request);
    Task<List<Subscription>> GetSubscriptions();
}