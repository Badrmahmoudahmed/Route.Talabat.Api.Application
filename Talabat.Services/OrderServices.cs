﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OredrAggregate;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
	public class OrderServices : IOrderService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IGenericRepository<Product> _productrepository;
		private readonly IGenericRepository<DeliveryMethod> _deliverymethodRepo;
		private readonly IGenericRepository<Order> _orderOrder;

		public OrderServices(IBasketRepository basketRepository,IGenericRepository<Product> Productrepository,IGenericRepository<DeliveryMethod> deliverymethodRepo,IGenericRepository<Order> orderOrder)
        {
			_basketRepository = basketRepository;
			_productrepository = Productrepository;
			_deliverymethodRepo = deliverymethodRepo;
			_orderOrder = orderOrder;
		}
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMehtodID, Adress ShippingAdress)
		{
			var basket = await _basketRepository.GetBasketAsync(basketId);
			var orderItems = new List<OrderItem>();
			if(basket?.BasketItems?.Count > 0)
			{
				foreach (var item in basket.BasketItems)
				{
					var product = await _productrepository.GetByIdAsync(item.Id);
					var productitem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
					var orderItem = new OrderItem(productitem, product.Price, item.Quantity);
					orderItems.Add(orderItem);
				}
			}
			var subtotal = orderItems.Sum(order => order.Price * order.Quantity);
			var deliverymethod = await _deliverymethodRepo.GetByIdAsync(deliveryMehtodID);

			var order = new Order(buyerEmail, ShippingAdress, deliverymethod, orderItems, subtotal);

			
		}

		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orederId)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}