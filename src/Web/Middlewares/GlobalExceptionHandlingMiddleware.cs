using Core.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Web.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    public GlobalExceptionHandlingMiddleware(
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AppValidationException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"¡Oops! Algo pasó: {ex.Message}");
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync($"¡Oops! Error interno del servidor: {ex.Message}");
        }
    }
}
