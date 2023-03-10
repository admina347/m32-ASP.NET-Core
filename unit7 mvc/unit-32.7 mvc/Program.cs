using Microsoft.EntityFrameworkCore;
using unit_32._7_mvc.Models.Db;
using unit_32._7_mvc.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false);

// Add services to the container.
builder.Services.AddControllersWithViews();

//БД
builder.Services.AddDbContext<BlogContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


// регистрация сервиса репозитория для взаимодействия с базой данных
builder.Services.AddScoped<IBlogRepository, BlogRepository>();


var app = builder.Build();

// Подключаем логирвоание с использованием ПО промежуточного слоя
app.UseMiddleware<LoggingMiddleware>();

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
/* app.MapControllerRoute(
    name: "Users",
    pattern: "{controller=Users}/{action=Index}/{id?}"); */

app.Run();
