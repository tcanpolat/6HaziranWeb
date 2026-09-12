using _14_Middleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*
 * Middleware Nedir?
 * Middleware bir web uygulamasında gelen http isteklerini işleyen ve yanıtları oluşturan yazılım
 * bileşenidir. ASP.NET Core'da middleware'ler, isteklerin işlenme sürecinde belirli işlerin
 * yapılmasını sağlar.
 * 
 * Middleware'ler uygulamanın istek işleme boru hattında (pipeline) oluşturulur ve her middleware,
 * isteği alır, üzerinde işlem yapar ve isteği bir sonraki middleware'e aktarır veya yanıtı 
 * döndürebilir.
 * Middleware'in bazı kullanım alanları
 * - Kimlik doğrulama ve yetkilendirme
 * - Hata işleme
 * - Statik dosya sunumu
 * - Günlük kaydı tutma (Log)
 * - Cors (Cross Origin Resource Sharing) yönetim
 
 */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<RequestTimingMiddleware>();
app.UseMiddleware<CustomAuthrarizationMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
