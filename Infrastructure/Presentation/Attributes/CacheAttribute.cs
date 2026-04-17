using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int durationInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;


            var cacheKey = GenerateCacheKey(context.HttpContext.Request);

         var result = await cacheService.GetCacheValueAysnc(cacheKey);

            if(!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result

                };

                return;
            }

            // Execute The Endpoint
        var contextResult =  await next.Invoke();

            if (contextResult.Result is OkObjectResult okObject)
            {
              await  cacheService.SetCacheValueAysnc(cacheKey, okObject.Value, TimeSpan.FromSeconds(durationInSec) );
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();

            key.Append(request.Path);

            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"|{item.Key} {item.Value}");
            }
            // api/Products?BrandId=1&Sort=pricedesc&PageSize=5&PageIndex=1

            // api/Products|BrandId=1|Sort=pricedesc|PageSize=5|PageIndex=1



            return key.ToString();
        }
    }
}
