using SubscriptionManager.Models;
using SubscriptionManager.Models.Requests;

namespace SubscriptionManager.Services.Interfaces;

public interface ISubscriptionService
{
    Task<Subscription> QuerySubscriptionById(int subscriptionId);
    public Subscription CreateSubscription(CreateSubscriptionRequest request);
    void RemoveSubscription(Subscription subscription);
}