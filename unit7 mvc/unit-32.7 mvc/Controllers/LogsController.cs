using Microsoft.AspNetCore.Mvc;
using unit_32._7_mvc.Repositories;

namespace unit_32._7_mvc.Controllers
{
    //[Route("[controller]")]
    public class LogsController : Controller
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ILogger<LogsController> _logger;

        public LogsController(ILogger<LogsController> logger, IRequestRepository requestRepository)
        {
            _logger = logger;
            _requestRepository = requestRepository;
        }

        public async Task <IActionResult> Index()
        {
            var requests = await _requestRepository.GetAllRequestsAsync();
            return View(requests);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}