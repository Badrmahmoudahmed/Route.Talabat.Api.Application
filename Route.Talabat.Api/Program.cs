
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
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
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			#endregion

			var app = builder.Build();

			using var scope = app.Services.CreateScope(); 
			var services = scope.ServiceProvider;
			var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				var dbContext = services.GetRequiredService<StoreContext>();
				await dbContext.Database.MigrateAsync();
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


			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}
