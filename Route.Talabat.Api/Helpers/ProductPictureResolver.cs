using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Api.Dtos;
using Talabat.Core.Entities;

namespace Route.Talabat.Api.Helpers
{
	public class ProductPictureResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
			}
			return string.Empty;

		}
	}
}
