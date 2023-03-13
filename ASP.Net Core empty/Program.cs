var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //app.UseDeveloperExceptionPage();
}


app.UseRouting();

// Поддержка статических файлов
app.UseStaticFiles();

//builder.WebHost.UseWebRoot("Views");

/* //Используем метод Use, чтобы запрос передавался дальше по конвейеру
app.Use(async (context, next) =>
{
   // Строка для публикации в лог
   string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
  
   // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
   string logFilePath = Path.Combine(app.Environment.ContentRootPath, "Logs", "RequestLog.txt");
  
   // Используем асинхронную запись в файл
   await File.AppendAllTextAsync(logFilePath, logMessage);
  
   await next.Invoke();
}); */

// Подключаем логирвоание с использованием ПО промежуточного слоя
app.UseMiddleware<LoggingMiddleware>();

/* //Добавляем компонент для логирования запросов с использованием метода Use.
app.Use(async (context, next) =>
{
    // Для логирования данных о запросе используем свойства объекта HttpContext
    Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
    await next.Invoke();
}); */


// Сначала используем метод Use, чтобы не прерывать ковейер
/* app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/config", async context =>
    {
        await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. App running configuration: {app.Environment.EnvironmentName}");
    });
}); */

//Добавляем компонент с настройкой маршрутов для главной страницы
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync($"Welcome to the {app.Environment.ApplicationName}!");
    });
});

// Все прочие страницы имеют отдельные обработчики
//app.Map("/about", About);
app.Map("/about", async (context) =>
{
    await context.Response.WriteAsync($"{app.Environment.ApplicationName} - ASP.Net Core tutorial project");
});
app.Map("/config", async (context) =>
{
    await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. App running configuration: {app.Environment.EnvironmentName}");
});


// Обработчик для ошибки "страница не найдена"
app.Run(async (context) =>
{
    int zero = 0;
    int result = 4 / zero;
    await context.Response.WriteAsync($"Page not found");
});

Console.WriteLine($"Launching project from: {app.Environment.ContentRootPath}");

//app.MapGet("/", () => $"Welcome to the {app.Environment.ApplicationName}!");

app.Run();
