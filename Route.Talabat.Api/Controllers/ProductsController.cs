using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Api.Dtos;
using Route.Talabat.Api.ErrorsHandler;
using Talabat.Core.Entities;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Specification;

namespace Route.Talabat.Api.Controllers
{

	public class ProductsController : BaseController
	{
		private readonly IGenericRepository<Product> _productRepository;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> ProductRepository ,IMapper mapper)
		{
			_productRepository = ProductRepository;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetAll()
		{
			var spec = new ProductWithBrandAndCategorySpecification();
			var products = await _productRepository.GetAllWithSpecAsync(spec);
			var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
			return Ok(MappedProducts);
		}


		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetById(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);
			var product = await _productRepository.GetByIdWithSpecAsync(spec);
			var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
			return Ok(MappedProduct);
		}

	}
}
