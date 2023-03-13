using unit_32._7_mvc.Models.Db;
using unit_32._7_mvc.Repositories;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private IRequestRepository _requestRepository;

    /* //Заполним модель
        Request newRequest = new Request(); */

    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        //_requestRepository = requestRepository;
    }

    private void LogConsole(HttpContext context)
    {
        // Для логирования данных о запросе используем свойста объекта HttpContext
        Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
    }

    private async Task LogFile(HttpContext context)
    {
        // Строка для публикации в лог
        string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

        // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
        string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");

        // Используем асинхронную запись в файл
        await File.AppendAllTextAsync(logFilePath, logMessage);
    }

    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    private async Task LogToDbAsync(HttpContext context)
    {
        //Заполним модель
        Request newRequest = new Request()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Url = $"http://{context.Request.Host.Value + context.Request.Path}"
        };
        
            /* newRequest.Id = Guid.NewGuid();
            newRequest.Date = DateTime.Now;
            newRequest.Url = $"http://{context.Request.Host.Value + context.Request.Path}"; */
        
        //Запись в БД
        if (newRequest != null && _requestRepository != null)
        {
            await _requestRepository.AddRequestToDbAsync(newRequest);
        }        
    }

    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context, IRequestRepository requestRepository)
    {
        //конструктор
        _requestRepository = requestRepository;
        LogConsole(context);
        await LogFile(context);
        //
        await LogToDbAsync(context);

        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
    }
}