using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services.Contract;

namespace Route.Talabat.Api.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timetolive;

        public CachedAttribute(int timetolive)
        {
            _timetolive = timetolive;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            
            var respone = await CachedService.GetCachedResponse(CacheKey);

            if(!string.IsNullOrEmpty(respone))
            {
                var result = new ContentResult()
                {
                    Content = respone,
                    ContentType = "application/json",
                    StatusCode = 200,
                };

                context.Result = result;
                return;
            }

           var executedactionresult =  await next.Invoke();

            if(executedactionresult.Result is OkObjectResult okObjectResult) 
            {
                await CachedService.CacheResponseAsync(CacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timetolive));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keybuilder = new StringBuilder();

            keybuilder.Append(request.Path);

            foreach (var (key, value) in request.Query.OrderBy(q => q.Key))
            {
                keybuilder.Append($"/{key}-{value}");
            }
            return keybuilder.ToString();
        }
    }
}
