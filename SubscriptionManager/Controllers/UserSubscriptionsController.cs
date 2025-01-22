using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionManager.Models;
using SubscriptionManager.Models.Requests;
using SubscriptionManager.Services.Interfaces;

namespace SubscriptionManager.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserSubscriptionsController : ControllerBase
{
    private readonly IUserSubscriptionsService _userSubscriptionsService;

    public UserSubscriptionsController(IUserSubscriptionsService userSubscriptionsService)
    {
        _userSubscriptionsService = userSubscriptionsService;
    }

    [HttpPost]
    public async Task<ActionResult<Subscription>> AddSubscription(CreateSubscriptionRequest request)
        => await _userSubscriptionsService.AddSubscription(request); 
    
    [HttpPatch]
    public async Task CancelSubscription(int subscriptionId)
        => await _userSubscriptionsService.CancelSubscription(subscriptionId);

    [HttpGet]
    public async Task<ActionResult<List<Subscription>>> GetSubscriptions()
        => await _userSubscriptionsService.GetSubscriptions();
}