using Route.Talabat.Api.Helpers;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;
using Talabat.Services;

namespace Route.Talabat.Api.Extenstion
{
	public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddAplicationServices(this IServiceCollection services) 
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositiry<>));
			services.AddScoped(typeof(IBasketRepository), typeof(BascketRepository));
			services.AddScoped(typeof(IAuthService),typeof(AuthService));
			services.AddAutoMapper(typeof(MappingProfiles));
			return services;
		}
	}
}
