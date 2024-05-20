using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Api.ErrorsHandler;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;

namespace Route.Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("{basketid}")]
        public async Task<ActionResult<CustmorBasket>> CreateOrUpdatePaymentIntent(string basketid)
        {
            var basket = await _paymentService.CreateorUpdatePaymentIntent(basketid);

            if (basket is null) return BadRequest(new ApiResponse(400, "An Error In The Basket"));

            return Ok(basket);
        }
    }
}
