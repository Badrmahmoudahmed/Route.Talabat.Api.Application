using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specification;

namespace Talabat.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitofWork _unitofWork;

		public ProductService(IUnitofWork unitofWork)
        {
			_unitofWork = unitofWork;
		}
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
		{
			var brands = await _unitofWork.Repository<ProductBrand>().GetAllAsync();
			return brands;
		}

		public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
		{
			var Categories = await _unitofWork.Repository<ProductCategory>().GetAllAsync();
			return Categories;
		}

		public async Task<Product> GetProductAsync(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);
			var product = await _unitofWork.Repository<Product>().GetByIdWithSpecAsync(spec);

			return product;
		}

	

		public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productSpecParams)
		{
			var spec = new ProductWithBrandAndCategorySpecification(productSpecParams);
			var products = await _unitofWork.Repository<Product>().GetAllWithSpecAsync(spec);
			return products;
		}
	}
}
