using Complaints.IBussiness;
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
        public ComplaintsController(IComplaintsManager complaintsManager) {
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
        public IActionResult Complaints(int currentPage=1, string searchParams="", int page = 1)
        {
            var result = GetComplaintsData(currentPage, searchParams,page);
            return View("Complaints",result);
        }
        public IActionResult CompalintsAddEdit()
        {
            return View();
        }
        public async Task<LiveComplaintsModel> GetViewComplaintById(int complaintId)
        {
            var complaintInfo =await _icomplaintsManager.GetViewComplaintById(complaintId);

            return complaintInfo;
        }
        public ComplaintsInfo GetComplaintById(int complaintId)
        {
            var complaintInfo = _icomplaintsManager.GetComplaintById(complaintId);
            return complaintInfo;
        }
        public IActionResult ComplaintDetails(string product="", string company = "", string searchParam="", int page=1)
        {
            var result = GetDetailsByProductCompany(product,company, searchParam, page);
            return View(result);
        }
        public string InsertComplaintDetailsAddEdit(string ModelObject)
        {
            var dataModel=JsonConvert.DeserializeObject<CompalintsDataModel>(ModelObject);
            var result=_icomplaintsManager.InsertComplaintDetailsAddEdit(dataModel);
            return result;
        }
        public List<ProductInfo> GetDistinctProductsList()
        {
            var productsList = _icomplaintsManager.GetDistinctProductsList();

            return productsList;
        }
        public List<CompanyInfo> GetDistinctCompaniesListForProduct(string productName)
        {
            var CompanyInfo = _icomplaintsManager.GetDistinctCompaniesListForProduct(productName);

            return CompanyInfo;
        }

        [HttpGet]
        public ComplaintModelData GetComplaintsData(int currentPage, string searchParams,int page)
        {
            var result= _icomplaintsManager.GetComplaintsData(currentPage, searchParams,page);
            return result;
        }
        [HttpGet]
        public ComplaintsInfoData GetDetailsByProductCompany(string product,string company, string searchParam,int page)
        {
            var result = _icomplaintsManager.GetDetailsByProductCompany(product,company, searchParam, page);
            return result;
        }
        [HttpPost]
        public string DeleteComplaintById(int complaintId)
        {
            var response = _icomplaintsManager.DeleteComplaintById(complaintId);

            return response;
        }
        [HttpGet]
        public List<ComplaintsGraphData> GetGraphData(string productName, string companyName)
        {
            var response = _icomplaintsManager.GetComplaintsGraphData(productName, companyName);

            return response;
        }
    }
}
