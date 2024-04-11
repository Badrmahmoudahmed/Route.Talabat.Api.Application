using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositiry.Contract;

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
			var products = await _productRepository.GetAllAsync();
			return Ok(products);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetAllById(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			return Ok(product);
		}

	}
}
