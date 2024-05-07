using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Api.Dtos;
using Route.Talabat.Api.ErrorsHandler;
using Route.Talabat.Api.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specification;

namespace Route.Talabat.Api.Controllers
{

	public class ProductsController : BaseController
	{
		private readonly IGenericRepository<Product> _productRepository;
		private readonly IGenericRepository<ProductBrand> _brands;
		private readonly IGenericRepository<ProductCategory> _categories;
		private readonly IMapper _mapper;
		private readonly IProductService _productService;

		public ProductsController(IGenericRepository<Product> ProductRepository ,IGenericRepository<ProductBrand> brands , IGenericRepository<ProductCategory> categories, IMapper mapper , IProductService productService)
		{
			_productRepository = ProductRepository;
			_brands = brands;
			_categories = categories;
			_mapper = mapper;
			_productService = productService;
		}
		[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAll([FromQuery] ProductSpecParams productSpecParams)
		{
			
			var products = await _productService.GetProductsAsync (productSpecParams);
			var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
			var count = await _productRepository.GetCountWithSpecAsync(new ProductWithFiltrationCountSpecification(productSpecParams));
			return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex,productSpecParams.Pagesize, count , MappedProducts));
		}


		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetById(int id)
		{
			var product = await _productService.GetProductAsync (id);
			var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
			return Ok(MappedProduct);
		}

		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
		{
			var brands = await _productService.GetBrandsAsync();
			return Ok(brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
		{
			var Categories = await _productService.GetCategoriesAsync();
			return Ok(Categories);
		}

	}
}
