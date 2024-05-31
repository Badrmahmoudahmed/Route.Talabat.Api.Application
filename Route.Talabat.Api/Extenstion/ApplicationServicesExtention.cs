using Microsoft.AspNetCore.Authentication.JwtBearer;
using Route.Talabat.Api.Helpers;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;
using Talabat.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core;
using Talabat.Services.PaymentService;
using Talabat.Services.CacheService;

namespace Route.Talabat.Api.Extenstion
{
	public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddAplicationServices(this IServiceCollection services) 
		{
			services.AddSingleton(typeof(IResponseCacheService), typeof(ResponeseCacheService));
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositiry<>));
			services.AddScoped(typeof(IProductService) , typeof(ProductService));
			services.AddScoped(typeof(IOrderService), typeof(OrderServices));
			services.AddScoped(typeof(IUnitofWork), typeof(UnitofWork));
			services.AddScoped(typeof(IBasketRepository), typeof(BascketRepository));
			services.AddScoped(typeof(IAuthService),typeof(AuthService));
			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
			o.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidIssuer = "https://localhost:7142/",
				ValidateAudience = true,
				ValidAudience = "MySecurityApiUser",
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StrongAuthenticationKeyToReachMoreThan256Bytes")),
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero,
			}
			) ;
			services.AddAutoMapper(typeof(MappingProfiles));
			return services;
		}
	}
}
