﻿using Microsoft.EntityFrameworkCore;
using SubscriptionManager.Context;
using SubscriptionManager.Models;
using SubscriptionManager.Models.Requests;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly SubscriptionManagerContext _context;
    private readonly IUserContextService _userContextService;

    public SubscriptionService(SubscriptionManagerContext context, IUserContextService userContextService)
    {
        _context = context;
        _userContextService = userContextService;
    }

    public async Task<Subscription> QuerySubscriptionById(int subscriptionId)
    {
        var subscription = await _context.Subscriptions.FirstOrDefaultAsync(sub => sub.Id == subscriptionId);

        if (subscription == null)
            throw new ArgumentException("Subscription not found.");

        return subscription;
    }

    public Subscription CreateSubscription(CreateSubscriptionRequest request)
    {
        var subscription = new Subscription
        {
            Name = request.Name,
            Price = request.Price,
            UserId = request.UserId,
            TimeSpan = request.TimeSpan
        };
        
        _context.Subscriptions.Add(subscription);
        return subscription;
    }

    public void RemoveSubscription(Subscription subscription) //TODO Only have access to your own subscriptions
    {
        var userId = _userContextService.GetUserId();

        if (userId != subscription.UserId)
            throw new ArgumentException("User is not subscribed to this subscription.");
        
        _context.Subscriptions.Remove(subscription);
        _context.SaveChanges();
    }
}