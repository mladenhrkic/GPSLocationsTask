using Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToProblemDetails<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Result is successful");
        }

        return new ObjectResult(new ProblemDetails
        {
            Status = GetStatusCode(result.Error.Type),
            Title = GetTitle(result.Error.Type),
            Extensions = { { "errors", new[] { result.Error } } }
        })
        {
            StatusCode = GetStatusCode(result.Error.Type)
        };
        

        static int GetStatusCode(Error.ErrorType errorType) =>
            errorType switch
            {
                Error.ErrorType.Validation => StatusCodes.Status400BadRequest,
                Error.ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                Error.ErrorType.NotFound => StatusCodes.Status404NotFound,
                Error.ErrorType.Failure => StatusCodes.Status500InternalServerError,
                Error.ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null)
            };

        static string GetTitle(Error.ErrorType errorType) =>
            errorType switch
            {
                Error.ErrorType.Validation => "Bad Request",
                Error.ErrorType.Unauthorized => "Unauthorized",
                Error.ErrorType.NotFound => "Not Found",
                Error.ErrorType.Failure => "Failure",
                Error.ErrorType.Conflict => "Conflict",
                _ => "Server Failure"
            };

    }
}