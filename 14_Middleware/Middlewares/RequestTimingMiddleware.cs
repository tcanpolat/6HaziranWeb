using System.Diagnostics;

namespace _14_Middleware.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // requestin başladığı zaman
            var watch = Stopwatch.StartNew();

            await _next(context);

            var elapsed = watch.ElapsedMilliseconds; // Geçen süreyi al

            Debug.WriteLine($"Request [{context.Request.Method}] --- {context.Request.Path} --- İşlem Süresi: {elapsed} ms");
        }
    }
}
