using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Universitas.Contracts.Exceptions;

namespace Universitas.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next) //puede haber mas de un middleware. RequestDelegate es
                                                                 //la proxima accion (por ej el porximo middleware) a ejecutarse O el controlador mismo 
                                                                 //si este es el ultimo middleware.
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context) //metodo a ejecutar al ejecutar el middleware. Representa el middleware
        {                                             // context es la request http PURA. Por ej, tiene propiedades como context.Request.Method (es el verbo http de la request), tiene la url, tiene el body, etc.
                                                      // El ultimo middleware, que es uno nativo de ASP.NET va a usarlo para definir a que controlador llamar y con que argumentos, y va a setear context.Response,
                                                      // que es la respuesta http con el body y el return code que le corresponda.
            try
            {
                await _next(context); //invoca al proximo middleware
            }
            catch (ExpectedException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.Code;

                await context.Response.WriteAsync((new ErrorDetails(ex.Message, ex.Code)).ToString());
            }
        }
    }

    class ErrorDetails
    {
        public string Message { get; set; }
        public int Code { get; set; }

        public ErrorDetails(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
