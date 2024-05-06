using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OredrAggregate;

namespace Talabat.Core.Services.Contract
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMehtodID, AdressOrder ShippingAdress);
		Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
		Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orederId);
		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
	}
}
