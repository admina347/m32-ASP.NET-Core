using Microsoft.AspNetCore.Mvc;
using unit_32._7_mvc.Repositories;

namespace unit_32._7_mvc.Controllers
{
    //[Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IBlogRepository _repo;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IBlogRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task <IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}