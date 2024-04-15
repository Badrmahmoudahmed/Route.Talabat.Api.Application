using Talabat.Core.Entities;

namespace Route.Talabat.Api.Dtos
{
	public class ProductToReturnDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Discription { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public int BrandId { get; set; }
		public string ProductCategory { get; set; }
		public string ProductBrand { get; set; }
	}
}
