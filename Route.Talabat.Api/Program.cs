
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Route.Talabat.Api.ErrorsHandler;
using Route.Talabat.Api.Helpers;
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
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositiry<>));
			builder.Services.AddAutoMapper(typeof(MappingProfiles));

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
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
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			//app.UseAuthorization();
			app.UseStaticFiles();

			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}
