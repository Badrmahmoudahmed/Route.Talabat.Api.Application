using AutoMapper;
using Route.Talabat.Api.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;

namespace Route.Talabat.Api.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.ProductCategory, O => O.MapFrom(S => S.ProductCategory.Name))
                .ForMember(D => D.PictureUrl , O => O.MapFrom<ProductPictureResolver>());
            CreateMap<CustmorBasketDto, CustmorBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<Adress, AdressDto>();
        }
    }
}
