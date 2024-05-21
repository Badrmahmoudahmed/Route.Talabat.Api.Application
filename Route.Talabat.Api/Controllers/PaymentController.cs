using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Api.ErrorsHandler;
using Stripe;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;

namespace Route.Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger _logger;
        private const string whsecret = "whsec_5704c980b0c19de87bcfa3e6efd479aae752e057299ea0b44ef4a49d637ed12e";
        public PaymentController(IPaymentService paymentService , ILogger logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet("{basketid}")]
        public async Task<ActionResult<CustmorBasket>> CreateOrUpdatePaymentIntent(string basketid)
        {
            var basket = await _paymentService.CreateorUpdatePaymentIntent(basketid);

            if (basket is null) return BadRequest(new ApiResponse(400, "An Error In The Basket"));

            return Ok(basket);
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], whsecret);
                var paymnetintent = (PaymentIntent) stripeEvent.Data.Object;
                // Handle the event
                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        await _paymentService.UpdateOrderStatus(paymnetintent.Id, true);
                        _logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        await _paymentService.UpdateOrderStatus(paymnetintent.Id, false);
                        _logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
                        break;
                        
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
