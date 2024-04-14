using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Shortify.Models;
using System.Text.Json;

namespace Shortify.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        private void HandleException(HttpContext context, Exception ex)
        {
            string routeWhereExceptionOccured = context.Request.Path;

            var path = JsonSerializer.Serialize(routeWhereExceptionOccured);
            var result = new ErrorViewModel
            {
                Path = path
            };

            result.ErrorMessage = ex switch
            {
                ValidationException e => string.Join(' ', e.Errors.Select(err => err.ErrorMessage)),
                _ => "Что-то пошло не так..."
            };

            string messageJson = JsonSerializer.Serialize(result);
            string redirectUrl = $"/Home/Error?messageJson={messageJson}";

            context.Response.Redirect(redirectUrl);
        }
    }
}
