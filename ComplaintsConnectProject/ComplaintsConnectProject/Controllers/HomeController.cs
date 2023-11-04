using Complaints.IBusiness;
using ComplaintsConnectProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ComplaintsConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IComplaintsManager _icomplaintsManager;

        IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IComplaintsManager icomplaintsManager)
        {
            _logger = logger;
            _icomplaintsManager = icomplaintsManager;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GetConsumerDatas()
        {
            return View();
        }
    }
}