using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Library.WebApplication.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                Log(httpContext, e);
                throw;
            }
        }
        private void Log(HttpContext context, Exception exception)
        {
            var savePath = @"C:\Users\Anuitex-11\source\repos\booksShop\App_exceptions\";
            var now = DateTime.Now;
            var fileName = $"{now.ToString("yyyy_MM_dd")}.log";
            var filePath = Path.Combine(savePath, fileName);

            new FileInfo(filePath).Directory.Create();

            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine($"{now.ToString("HH:mm:ss")} {context.Request.Path}");
                writer.WriteLine(exception.Message);
            }
        }
    }
}

    
