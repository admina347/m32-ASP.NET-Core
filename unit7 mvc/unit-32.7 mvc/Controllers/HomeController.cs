using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using unit_32._7_mvc.Models;
using unit_32._7_mvc.Models.Db;
using unit_32._7_mvc.Repositories;

namespace unit_32._7_mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    // ссылка на репозиторий
    private readonly IBlogRepository _repo;

    public HomeController(ILogger<HomeController> logger, IBlogRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    /* public IActionResult Index()
    {
        return View();
    } */
    public async Task<IActionResult> Index()
    {
        /* // Добавим создание нового пользователя
        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Andrey",
            LastName = "Petrov",
            JoinDate = DateTime.Now
        };

        // Добавим в базу
        await _repo.AddUser(newUser);

        // Выведем результат
        Console.WriteLine($"User with id {newUser.Id}, named {newUser.FirstName} was successfully added on {newUser.JoinDate}"); */

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    //users list
    /* public async Task<IActionResult> Authors()
    {
        var authors = await _repo.GetUsers();
        return View(authors);
    } */

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
