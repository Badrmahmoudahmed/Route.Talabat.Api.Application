
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualBasic;
using Route.Talabat.Api.ErrorsHandler;
using Route.Talabat.Api.Extenstion;
using Route.Talabat.Api.Helpers;
using Route.Talabat.Api.Middlewares;
using StackExchange.Redis;
using Talabat.Core.Repositiry.Contract;
using Talabat.Infrastructure;
using Talabat.Infrastructure.Data;

namespace Route.Talabat.Api
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Services Configuration
			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddDbContext<StoreContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConntection")); });
			builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			} 
			);
			builder.Services.AddAplicationServices();


			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddSwaggerServices();

			#region ConfigureValidationError
			builder.Services.Configure<ApiBehaviorOptions>(option =>
				{
					option.InvalidModelStateResponseFactory = (actioncontext) =>
					{
						var erros = actioncontext.ModelState.Where(p => p.Value.Errors.Count > 0)
						.SelectMany(p => p.Value.Errors)
						.Select(E => E.ErrorMessage)
						.ToList();

						var response = new ApiValidationErrorResponse()
						{
							Errors = erros
						};

						return new BadRequestObjectResult(response);
					};


				}); 
			#endregion
			#endregion

			var app = builder.Build();

			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				var dbContext = services.GetRequiredService<StoreContext>();
				await dbContext.Database.MigrateAsync();
				await StoreContextSeed.seedAsync(dbContext);
			}
			catch (Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an errorr occured during apply the migration");
			}

			#region Add Kesteral Middllwears
			app.UseMiddleware<ExptionMiddleware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddleWare();
			}
			app.UseStatusCodePagesWithReExecute("/Errors/{0}");
			app.UseHttpsRedirection();

			//app.UseAuthorization();
			app.UseStaticFiles();

			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}
