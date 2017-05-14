using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TestSupport
{
    public abstract class TestServerResponse
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.Run(context =>
            {
                var response = Response();
                context.Response.ContentLength = response.Length;
                return context.Response.WriteAsync(response);
            });
        }

        public abstract string Response();
    }
}