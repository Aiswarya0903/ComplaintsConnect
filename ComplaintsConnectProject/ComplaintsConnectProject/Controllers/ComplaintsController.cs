﻿

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
        public IActionResult Complaints(int currentPage = 1, string searchParams = "", int page = 1)
        {
            var result = GetComplaintsData(currentPage, searchParams, page);
            return View("Complaints", result);
        }

        [HttpGet]
        public ComplaintModelData GetComplaintsData(int currentPage, string searchParams, int page)
        {
            var result = _icomplaintsManager.GetComplaintsData(currentPage, searchParams, page);
            return result;
        }

        public IActionResult CompalintsAddEdit()
        {
            return View();
        }
        public async Task<LiveComplaintsModel> GetViewComplaintById(int complaintId)
        {
            var complaintInfo = await _icomplaintsManager.GetViewComplaintById(complaintId);

            return complaintInfo;
        }
        public ComplaintsInfo GetComplaintById(int complaintId)
        {
            var complaintInfo = _icomplaintsManager.GetComplaintById(complaintId);
            return complaintInfo;
        }

    }
}
