

using Talabat.Core.Entities.OredrAggregate;

namespace Route.Talabat.Api.Dtos
{
	public class OrderDto
	{
		public string BuyerEmail { get; set; }
        public string BasketId { get; set; }
		public int DeliveryMethodId { get; set; }
        public AdressOrder ShippingAdress { get; set; }
    }
}
