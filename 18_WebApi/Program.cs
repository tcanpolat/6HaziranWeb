using _18_WebApi.DataContext;
using _18_WebApi.Seeder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProductContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))    
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(
    options =>
    {
        {
            options.AddDefaultPolicy(
             builder =>
             {
                 builder.AllowAnyOrigin() // AllowAnyOrigin() metodu, herhangi bir kaynaktan gelen isteklere izin verir. Bu, tüm alan adlarından gelen isteklerin kabul edileceği anlamına gelir.
                        .AllowAnyHeader() // AllowAnyHeader() metodu, herhangi bir başlıkla gelen isteklere izin verir. Bu, tüm HTTP başlıklarının kabul edileceği anlamına gelir.
                        .AllowAnyMethod(); // AllowAnyMethod() metodu, herhangi bir HTTP yöntemiyle gelen isteklere izin verir. Bu, GET, POST, PUT, DELETE gibi tüm HTTP yöntemlerinin kabul edileceği anlamına gelir.
             });
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ProductContext>();
    ProductSeeder.Seed(context);

};
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
