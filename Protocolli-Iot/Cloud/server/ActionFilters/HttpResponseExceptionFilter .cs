using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Server.ActionFilters;

internal class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not null)
        {
            context.Result = new ObjectResult(new DefaultMessage(500,context.Exception.Message))
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}