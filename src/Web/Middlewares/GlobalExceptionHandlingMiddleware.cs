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
        Console.WriteLine("Hola, yendo para rutear");
        try
        {
            await next(context);
        }
        catch (AppValidationException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync($"Error interno del servidor: {ex.Message}");
        }
        Console.WriteLine("Hola, volviendo para responder");
    }
}
