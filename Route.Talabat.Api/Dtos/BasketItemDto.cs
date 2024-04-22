using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.Api.Dtos
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		public string pictureUrl { get; set; }
		[Required]
		[Range(0.1, double.MaxValue , ErrorMessage = "Price Must Be Greater Than Zero")]
		public decimal Price { get; set; }
		[Required]
		public string Category { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be Greater Than One")]
		public int Quantity { get; set; }
	}
}