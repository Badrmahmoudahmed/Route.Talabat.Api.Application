namespace Route.Talabat.Api.Extenstion
{
	public static class SwaggerServicesExtention
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}

		public static WebApplication UseSwaggerMiddleWare(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}

	}
}
