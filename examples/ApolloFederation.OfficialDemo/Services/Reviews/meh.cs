using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Reviews
{
    public class meh
    {
        private readonly RequestDelegate _next;

        public meh(RequestDelegate next)
        {
            _next = next;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }

        public static async Task<string> ReadBody(HttpContext httpContext)
        {
            var bodyStr = "";
            var req = httpContext.Request;


            // Arguments: Stream, Encoding, detect encoding, buffer size
            // AND, the most important: keep stream opened
            using (StreamReader reader
                = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
            }

            return bodyStr;
        }
    }
}
