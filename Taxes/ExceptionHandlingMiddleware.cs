using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Taxes.Core.Exceptions;

namespace Taxes.Api
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
 
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
 
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException e)
            {
                var statusCode = e.Type switch
                {
                    DomainExceptionType.AlreadyExists => 409,
                    DomainExceptionType.NotFound => 404,
                    _ => 400
                };
                await WriteErrorResponse(context.Response, statusCode, e.Type.ToString(), e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occured in method {context.Request.GetDisplayUrl()}");
                await WriteErrorResponse(context.Response, 500, "UnexpectedError", "An unexpected error has occured");
            }
        }

        private static async Task WriteErrorResponse(HttpResponse response, int statusCode, string errorCode, string description)
        {
            response.StatusCode = statusCode;
            response.ContentType = "application/json";

            var responseObject = new ErrorResponse
            {
                Code = errorCode,
                Description = description
            };

            await response.WriteAsync(JsonConvert.SerializeObject(responseObject));
        }
    }
}