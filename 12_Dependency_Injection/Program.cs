using _12_Dependency_Injection.Services.Abstract;
using _12_Dependency_Injection.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/* 
 LifeCycle (Yaşam Döngüsü) Yöntemi:
IMyService ve MyService' i container'a eklememiz gerekiyor
NET Core Dependency Injection servislerinin ömrü (life cycle) belirlemek için 3 farklı yöntem sunar:
AddSingelton,AddScoped,AddTransient => Her bir birinin avantajları ve dezantajları.
1. AddTransient:
    - Her bir istek için yeni bir örneklem oluştur.
    - Kısa ömürlü servisler için uygundur.
    - Hafıza kullanımı çok yüksektir çünkü her istek yeni bir nesne oluştur.
2. AddScoped:
    - Her istek (HTTP request) için tek bir örnek oluşturur.
    - Web uygumalarında yaygın olarak kullanılır.
    - Aynı istek içinde paylaşılan veriler için uygundur.
3. AddSingelon:
    - Uygulama ömrü boyunca tek bir örneklem oluşturur.
    - Hafıza kullanımı düşüktür çünkü tek bir nesne kullanılır.
    - Paylaşılan durum (state) yöntemi için dikkatli kullanılmalıdır, çünkü tüm istekler aynı nesneyi kullanılır.
 */
builder.Services.AddScoped<IMyService, MyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
