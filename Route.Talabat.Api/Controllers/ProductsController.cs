using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Specification;

namespace Route.Talabat.Api.Controllers
{

	public class ProductsController : BaseController
	{
		private readonly IGenericRepository<Product> _productRepository;
		public ProductsController(IGenericRepository<Product> ProductRepository)
		{
			_productRepository = ProductRepository;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetAll()
		{
			var spec = new ProductWithBrandAndCategorySpecification();
			var products = await _productRepository.GetAllWithSpecAsync(spec);
			return Ok(products);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetById(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);
			var product = await _productRepository.GetByIdWithSpecAsync(spec);
			return Ok(product);
		}

	}
}
