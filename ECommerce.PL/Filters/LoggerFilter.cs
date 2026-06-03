using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.PL
{
    public class LoggerFilter: Attribute, IAsyncActionFilter
    {
        private readonly ILogger<LoggerFilter> _logger;
    public LoggerFilter(ILogger<LoggerFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionName = context.ActionDescriptor.DisplayName;

        // Do something before the action executes.
        _logger.LogInformation($"\nAction {actionName} is executing and Started At {DateTime.Now}.");
        await next();

        // Do something after the action executes.
        _logger.LogInformation($"\nAction {actionName} has finished executing at {DateTime.Now}.");

    }
}
}
