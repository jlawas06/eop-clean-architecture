using EOP.Application.Dtos;
using EOP.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EOP.Application.Middlewares
{
    public class EOPErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public EOPErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (EOPException ex)
            {
                await HandleCustomExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsJsonAsync(new ErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            });
        }

        private static Task HandleCustomExceptionAsync(HttpContext context, EOPException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.StatusCode;

            return context.Response.WriteAsJsonAsync(new ErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            });
        }
    }
}
