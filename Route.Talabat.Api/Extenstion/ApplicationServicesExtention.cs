using Microsoft.AspNetCore.Authentication.JwtBearer;
using Route.Talabat.Api.Helpers;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;
using Talabat.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core;

namespace Route.Talabat.Api.Extenstion
{
	public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddAplicationServices(this IServiceCollection services) 
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositiry<>));
			services.AddScoped(typeof(IUnitofWork), typeof(UnitofWork));
			services.AddScoped(typeof(IBasketRepository), typeof(BascketRepository));
			services.AddScoped(typeof(IAuthService),typeof(AuthService));
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
