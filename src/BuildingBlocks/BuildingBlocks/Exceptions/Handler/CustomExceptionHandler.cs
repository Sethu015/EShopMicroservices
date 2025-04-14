using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler (ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error message {exceptionMessage} , Time of occurance {time}", exception.Message, DateTime.UtcNow);
            var message = exception.Message;
            var title = exception.GetType().Name;
            int StatusCode = exception switch
            {
                InternalServerErrorException =>
                (
                    StatusCodes.Status500InternalServerError
                ),
                BadRequestException => 
                (
                    StatusCodes.Status400BadRequest
                ),
                ValidationException =>
                (
                    StatusCodes.Status400BadRequest
                ),
                NotFoundException => 
                (
                    StatusCodes.Status404NotFound
                ),
                _ => 
                (
                    StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = StatusCode,
                Detail = message,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationException", validationException.Errors);
            }

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
