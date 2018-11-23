using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Todo.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var responseMessage = CreateBaseResponse(ex);
                await GenerateResponseObjectAsync(context, responseMessage);
                //_logger.LogError("ExceptionHandler middleware catched an error. Exception: {ex}", ex);
            }
        }

        private Response CreateBaseResponse(Exception ex)
        {
            var resp = new Response()
            {
                StatusCode = 400,
                Message = ex.Message
            };
            return resp;
        }

        private Task GenerateResponseObjectAsync(HttpContext context, Response message)
        {
            var body = new MemoryStream();
            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            body.Write(buffer, 0, buffer.Length);
            body.Position = 0;

            context.Response.Headers.Add("Content-Type", "application/json");
            context.Response.StatusCode = message.StatusCode.Value;

            return body.CopyToAsync(context.Response.Body);
        }
    }

    public static class MiddleWareExtentions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }

    public class Response
    {
        public int? StatusCode { get; set; }
        public string Message { get; set; }
    }
}
