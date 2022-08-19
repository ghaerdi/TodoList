using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using todolist.Helpers;

namespace todolist.Filters;

public class ErrorFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<ErrorFilter> _logger;

    public ErrorFilter(IHostEnvironment hostEnvironment, ILogger<ErrorFilter> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        string error = context.Exception.ToString();
        ResponseHelper<string> response = new(MessageHelper.Error.Unknown, error, true);
        context.Result = new BadRequestObjectResult(response);

        if (!_hostEnvironment.IsDevelopment())
            return;

        _logger.LogError(error);
    }
}
