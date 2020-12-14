using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NazirCevik.WebApiDemo.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //basic nazir:12345
            string autHeader = httpContext.Request.Headers["Authorization"];
            //varsa ve basic ile başlıyorsa
            if(autHeader=="null")
            {
                await _next(httpContext);
                return;
            }
            if (autHeader != null && autHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = autHeader.Substring(6).Trim();
                var credentialString = "";
                try
                {
                    credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }
                catch
                {
                    httpContext.Response.StatusCode = 500;
                }
                var credentials = credentialString.Split(':');
                if (credentials[0] == "nazir" && credentials[1] == "12345")
                {
                    var claims = new[] {new Claim("name",credentials[0]) ,
                    new Claim(ClaimTypes.Role,"admin")
                    };
                    var identity = new ClaimsIdentity(claims, "basic");
                    httpContext.User = new ClaimsPrincipal(identity);
                }
            }
            else
            {
                httpContext.Response.StatusCode = 401;
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
