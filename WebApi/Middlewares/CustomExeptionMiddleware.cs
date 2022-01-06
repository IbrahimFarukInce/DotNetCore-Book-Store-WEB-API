using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExeptionMiddleware
    {
        //Loglama ve hata yakalama
        private readonly RequestDelegate _next;
        private readonly ILoggerService _service;
        public CustomExeptionMiddleware(RequestDelegate next, ILoggerService service)
        {
            _next = next;
            _service = service;
        }

        public async Task Invoke(HttpContext context)
        {
            //Gelen requestin ne olduğunu ve endpointini alıyoruz
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path;
                _service.Write(message);
                await _next(context);
                watch.Stop();
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " Responsed " + context.Response.StatusCode + " in: " +watch.Elapsed.TotalMilliseconds+"ms";
                _service.Write(message);
                 
            }
            catch (System.Exception ex)
            {
                watch.Stop();
                await HandlException(context, ex, watch);
            }     
        }

        private Task HandlException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "aplication/json" ; //Response Geri Dönüş tipi
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest; //Response status code

            string message = "[Error]    HTTP " + context.Request.Method +" - "+ context.Response.StatusCode + "Error Message" + ex.Message + " in: " +watch.Elapsed.TotalMilliseconds+"ms";
            _service.Write(message);

            var result = JsonConvert.SerializeObject(new {error = ex.Message},Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExeptionMiddlewareExtention
    {
        public static IApplicationBuilder UseCustomExeptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExeptionMiddleware>();
        }
    }
}