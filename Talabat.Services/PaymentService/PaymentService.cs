using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OredrAggregate;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _bascketRepository;
        private readonly IUnitofWork _unitofWork;

        public PaymentService(IConfiguration configuration ,IBasketRepository bascketRepository, IUnitofWork unitofWork)
        {
            _configuration = configuration;
            _bascketRepository = bascketRepository;
            _unitofWork = unitofWork;
        }
        public async Task<CustmorBasket> CreateorUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:Secretkey"];
            var basket = await _bascketRepository.GetBasketAsync(basketId);

            if (basket is null) return null;

            var ShipingPrice = 0m;
            if(basket.DeliveryMethodId.HasValue) 
            {
                var DeliveryMethod = await _unitofWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShipingPrice = DeliveryMethod.Cost;
                basket.ShippingPrice = ShipingPrice;
            }

            if(basket.BasketItems.Count > 0)
            {
                var productRepo = _unitofWork.Repository<Product>();
                foreach (var item in basket.BasketItems)
                {
                    var product = await productRepo.GetByIdAsync(item.Id);
                    if (product.Price != item.Price)
                        item.Price = product.Price;
                }
            }
            PaymentIntent paymentIntent;
            var paymentintentservice = new PaymentIntentService();
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(p => p.Price * p.Quantity * 100) + (long)basket.ShippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
              paymentIntent =  await paymentintentservice.CreateAsync(Options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
    
            }
            else
            {
                var UpdateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(p => p.Price * p.Quantity * 100) + (long)basket.ShippingPrice * 100,
                };

                await paymentintentservice.UpdateAsync(basket.PaymentIntentId, UpdateOptions);
            }
           await _bascketRepository.UpdateBasketAsync(basket);

            return basket;
        }
    }
}
