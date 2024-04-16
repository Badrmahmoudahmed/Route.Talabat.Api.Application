using Route.Talabat.Api.ErrorsHandler;
using System.Net;
using System.Text.Json;

namespace Route.Talabat.Api.Middlewares
{
	public class ExptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExptionMiddleware> _loggerFactory;
		private readonly IWebHostEnvironment _env;

		public ExptionMiddleware(RequestDelegate next,ILogger<ExptionMiddleware> loggerFactory ,IWebHostEnvironment env )
        {
			_next = next;
			_loggerFactory = loggerFactory;
			_env = env;
		}
        public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next.Invoke(httpContext);
			}
			catch (Exception ex)
			{
				_loggerFactory.LogError(ex.Message);

				httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";
				var Response = _env.IsDevelopment() ? new ApiExptionResponse(httpContext.Response.StatusCode, ex.Message, ex.StackTrace)
					:
					new ApiExptionResponse(httpContext.Response.StatusCode);

				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				await httpContext.Response.WriteAsync(JsonSerializer.Serialize(Response , options));
			}
		}
	}
} 
