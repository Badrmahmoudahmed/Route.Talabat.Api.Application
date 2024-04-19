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
		private readonly IGenericRepository<ProductBrand> _brands;
		private readonly IGenericRepository<ProductCategory> _categories;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> ProductRepository ,IGenericRepository<ProductBrand> brands , IGenericRepository<ProductCategory> categories, IMapper mapper)
		{
			_productRepository = ProductRepository;
			_brands = brands;
			_categories = categories;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetAll()
		{
			var spec = new ProductWithBrandAndCategorySpecification();
			var products = await _productRepository.GetAllWithSpecAsync(spec);
			var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IEnumerable<ProductToReturnDto>>(products);
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

		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
		{
			var brands = await _brands.GetAllAsync();
			return Ok(brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
		{
			var Categories = await _categories.GetAllAsync();
			return Ok(Categories);
		}

	}
}
