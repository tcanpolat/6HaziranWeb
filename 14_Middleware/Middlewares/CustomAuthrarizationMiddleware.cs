namespace _14_Middleware.Middlewares
{
    public class CustomAuthrarizationMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomAuthrarizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            // privacy sayfası için basit bir yetki kontrolü
            if (context.Request.Path.StartsWithSegments("/Home/Privacy"))
            {
                // Örnek yetki kontrolü aslında User adlı bir property var bunu
                // kullanmak gerekir gerçek uygulamalarda User'ı kullanıyor.
                bool isAdmin =  context.User.IsInRole("Admin") ||
                                context.Request.Query.ContainsKey("isAdmin") &&
                                context.Request.Query["isAdmin"] == "true";

                if (!isAdmin)
                {
                    context.Response.Redirect("/Home/AccessDenied");
                    return;
                }
            }
            await _next(context);
        
        }
    }
}
