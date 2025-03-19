using SubscriptionManager.Context;
using SubscriptionManager.Models;
using SubscriptionManager.Models.Requests;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Services;

public class UserSubscriptionsService : IUserSubscriptionsService
{
    private readonly IUserService _userService;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IDiscountService _discountService;
    private readonly SubscriptionManagerContext _context;

    public UserSubscriptionsService(SubscriptionManagerContext context, IUserService userService,
        ISubscriptionService subscriptionService, IDiscountService discountService)
    {
        _context = context;
        _userService = userService;
        _subscriptionService = subscriptionService;
        _discountService = discountService;
    }

    public async Task CancelSubscription(int subscriptionId)
    {
        var user = await _userService.QueryPersonalAccount();
        var subscription = user.GetSubscriptionId(subscriptionId);

        if (subscription == null) throw new ArgumentException("Subscription not found.");

        user.CancelSubscription(subscription);

        _context.Users.Update(user);
        _context.Subscriptions.Update(subscription);

        await _context.SaveChangesAsync();
    }

    public async Task<Subscription> AddSubscription(CreateSubscriptionRequest request)
    {
        var user = await _userService.QueryPersonalAccount();
        var subscription = _subscriptionService.CreateSubscription(request);

        user.AddSubscription(subscription);

        _context.Users.Update(user);
        _context.Subscriptions.Update(subscription);
        await _context.SaveChangesAsync();

        return subscription;
    }

    public async Task ApplyDiscountOnSubscription(DiscountRequest request)
    {
        var subscription = await _subscriptionService.QuerySubscriptionById(request.SubscriptionId);
        var discount = _discountService.CreateDiscount(request);

        subscription.ApplyDiscount(discount);
        
        _context.Subscriptions.Update(subscription);
        _context.Discounts.Update(discount);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Subscription>> GetSubscriptions()
    {
        var user = await _userService.QueryPersonalAccount();
        return user.Subscriptions;
    }
}