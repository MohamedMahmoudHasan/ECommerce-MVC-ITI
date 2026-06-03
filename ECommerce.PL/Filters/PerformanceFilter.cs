using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ECommerce.PL
{
    public class PerformanceFilter : Attribute ,IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("\nBefore action executes.");
            var stopwatch = Stopwatch.StartNew();

            await next();

            stopwatch.Stop();
            Console.WriteLine($"\nAfter action executes. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
