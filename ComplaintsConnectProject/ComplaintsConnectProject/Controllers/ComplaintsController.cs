

using Complaints.IBusiness;
using Complaints.Models;
using Complaints.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ComplaintsConnect.Controllers
{
    public class ComplaintsController : Controller
    {
        IComplaintsManager _icomplaintsManager;
        public ComplaintsController(IComplaintsManager complaintsManager)
        {
            _icomplaintsManager = complaintsManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Home page
        /// </summary>
        /// <returns></returns>
        public IActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// AboutUs Page
        /// </summary>
        /// <returns></returns>
        public IActionResult AboutUs()
        {
            return View();
        }
        /// <summary>
        /// Compalints Page
        /// </summary>
        /// <returns></returns>
       
    }
}
