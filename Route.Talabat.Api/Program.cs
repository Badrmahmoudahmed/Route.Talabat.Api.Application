
using Microsoft.EntityFrameworkCore;
using Talabat.Infrastructure.Data;

namespace Route.Talabat.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Services Configuration
			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddDbContext<StoreContext>(options =>{ options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConntection"));});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(); 
			#endregion

			var app = builder.Build();

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
