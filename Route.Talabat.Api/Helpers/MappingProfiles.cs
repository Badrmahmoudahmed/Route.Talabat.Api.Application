using AutoMapper;
using Route.Talabat.Api.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.OredrAggregate;


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
            //CreateMap<AdressDto,AdressOrder>();
            CreateMap<Order, OrderToReurnDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(S => S.Product.PictureUrl));
        }
    }
}
