using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OredrAggregate;

namespace Talabat.Infrastructure.Data
{
	public static class StoreContextSeed
	{
		public static async Task seedAsync(StoreContext _dbContext)
		{
			if (!_dbContext.ProductBrands.Any())
			{

				var BrandsData = File.ReadAllText("../Talabat.Infrastructure/Data/DataSeed/brands.json");

				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

				if (Brands?.Count > 0)
				{
					foreach (var Brand in Brands)
						await _dbContext.Set<ProductBrand>().AddAsync(Brand);

					await _dbContext.SaveChangesAsync();
				}
			}
			if (!_dbContext.ProductCategories.Any())
			{

				var CategoriesData = File.ReadAllText("../Talabat.Infrastructure/Data/DataSeed/categories.json");

				var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

				if (Categories?.Count > 0)
				{
					foreach (var Category in Categories)
						await _dbContext.Set<ProductCategory>().AddAsync(Category);

					await _dbContext.SaveChangesAsync();
				}
			}
			if (!_dbContext.Products.Any())
			{

				var ProductsData = File.ReadAllText("../Talabat.Infrastructure/Data/DataSeed/products.json");

				var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

				if (Products?.Count > 0)
				{
					foreach (var product in Products)
						await _dbContext.Set<Product>().AddAsync(product);

					await _dbContext.SaveChangesAsync();
				}
			}
			if (!_dbContext.DeliveryMethods.Any())
			{
				var DeliveryMethodData = File.ReadAllText("../Talabat.Infrastructure/Data/DataSeed/delivery.json");
				var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
				if (DeliveryMethods?.Count > 0)
				{
					foreach (var DM in DeliveryMethods)
					{
						await _dbContext.Set<DeliveryMethod>().AddAsync(DM);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

		}
	}
}
