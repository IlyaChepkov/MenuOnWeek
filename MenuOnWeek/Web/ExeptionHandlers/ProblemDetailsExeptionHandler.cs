using MenuOnWeek.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MenuOnWeek.Web.ExeptionHandlers;

internal sealed class ProblemDetailsExeptionHandler : IExceptionHandler
{
    ProblemDetailsFactory problemDetailsFactory;

    public ProblemDetailsExeptionHandler(ProblemDetailsFactory problemDetailsFactory)
    {
        this.problemDetailsFactory = problemDetailsFactory;
    }

    async ValueTask<bool> IExceptionHandler.TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        int statusCode = exception switch
        {
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext, statusCode);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails);
        return true;
    }
}
