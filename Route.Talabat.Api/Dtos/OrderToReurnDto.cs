using Talabat.Core.Entities.OredrAggregate;

namespace Route.Talabat.Api.Dtos
{
	public class OrderToReurnDto
	{
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } 
		public string Statues { get; set; } 
		public AdressOrder ShippingAdress { get; set; }
		public string DeliveryMethod { get; set; }
		public decimal DeliveryMethodCost { get; set; }
		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
		public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public string PaymetnIntentId { get; set; } = string.Empty;
	}
}
