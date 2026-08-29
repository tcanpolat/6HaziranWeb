var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

// Rotaların tanımlandığı kısım
app.MapControllerRoute(
    name: "default", // varsayılan rota
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Rota Deseni

app.MapControllerRoute(
    name: "about", // about rotası
    pattern: "about", // Rota Deseni
    defaults: new { controller = "Home", action = "About" }); // Varsayılan controller ve action

app.MapControllerRoute(
    name: "aboutDetail", // aboutDetail rotası
    pattern: "about/detail/{id?}", // Rota Deseni
    defaults: new { controller = "Home", action = "AboutDetail" }
);

app.Run();
