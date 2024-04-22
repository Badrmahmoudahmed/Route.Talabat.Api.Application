using Route.Talabat.Api.Helpers;
using Talabat.Core.Repositiry.Contract;
using Talabat.Infrastructure;

namespace Route.Talabat.Api.Extenstion
{
	public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddAplicationServices(this IServiceCollection services) 
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositiry<>));
			services.AddScoped(typeof(IBasketRepository), typeof(BascketRepository));
			services.AddAutoMapper(typeof(MappingProfiles));
			return services;
		}
	}
}
