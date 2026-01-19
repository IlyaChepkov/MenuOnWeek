using MenuOnWeek.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MenuOnWeek.Web.ExeptionHandlers
{
    internal sealed class LoggerExeptionHandler : IExceptionHandler
    {
        ILogger logger;

        public LoggerExeptionHandler(ILogger<LoggerExeptionHandler> logger)
        {
            this.logger = logger;
        }

        async ValueTask<bool> IExceptionHandler.TryHandleAsync(HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is ValidationException || exception is KeyNotFoundException)
            {
                logger.LogInformation(exception.Message);
            }
            else
            {
                logger.LogError(exception.Message);
            }

            return await ValueTask.FromResult(false);
        }
    }
}
