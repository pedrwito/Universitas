﻿namespace Universitas.API.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            IHeaderDictionary headers = context.Request.Headers;
            string authentication = (string)headers["authentication"];

            if (authentication[0] == '*') 
            {
                string[] emailPassword = authentication.Split(':');

                string email = emailPassword[0].Substring(1);
                string password = emailPassword[1];
                
                // chequear que sea correcto y corresponda a un usuario

            }

            else
            {

            }

            await _next(context);
        }
    }
}
