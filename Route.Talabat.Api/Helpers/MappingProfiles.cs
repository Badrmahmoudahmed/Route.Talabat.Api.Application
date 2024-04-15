using AutoMapper;
using Route.Talabat.Api.Dtos;
using Talabat.Core.Entities;

namespace Route.Talabat.Api.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.ProductCategory, O => O.MapFrom(S => S.ProductCategory.Name));
        }
    }
}
