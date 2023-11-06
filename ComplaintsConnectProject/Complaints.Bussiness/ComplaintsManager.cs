using Complaints.IBussiness;
using Complaints.IData;
using Complaints.Models;
using Complaints.Models.Models;
using Newtonsoft.Json;
using RestSharp;
//using Complaints.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Complaints.Bussiness
{
    public class ComplaintsManager : IComplaintsManager
    {

        IComplaintsRepository _icomplaintsRepository;
        public ComplaintsManager(IComplaintsRepository icomplaintsRepository)
        {
            _icomplaintsRepository = icomplaintsRepository;
        }

        public string InsertComplaintDetailsAddEdit(CompalintsDataModel modelObject)
        {

            var result = _icomplaintsRepository.InsertComplaintDetailsAddEdit(modelObject);
            return result;
        }
        public List<ProductInfo> GetDistinctProductsList()
        {
            var productList = _icomplaintsRepository.GetDistinctProductsList();
            return productList;
        }
        public async Task<LiveComplaintsModel> GetViewComplaintById(int complaintId)
        {
            try
            {
                var options = new RestClientOptions("https://www.consumerfinance.gov")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/data-research/consumer-complaints/search/api/v1/" + complaintId + "", Method.Get);
                request.AddHeader("accept", "application/json");
                RestResponse response = await client.ExecuteAsync(request);
                Root result = JsonConvert.DeserializeObject<Root>(response.Content);
                Console.WriteLine(response.Content);
                return result.hits.hits.Count!=0 ? result.hits.hits[0]._source : null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public ComplaintsInfo GetComplaintById(int complaintId)
        {
            return _icomplaintsRepository.GetComplaintById(complaintId);
        }
        public List<CompanyInfo> GetDistinctCompaniesListForProduct(string productName)
        {
            return _icomplaintsRepository.GetDistinctCompaniesListForProduct(productName);
        }
        public ComplaintModelData GetComplaintsData(int currentPage, string searchParams,int page)
        {
            return _icomplaintsRepository.GetComplaintsData(currentPage, searchParams,page);
        }
        public ComplaintsInfoData GetDetailsByProductCompany(string product, string company, string searchParam, int currentPage)
        {
            return _icomplaintsRepository.GetDetailsByProductCompany(product, company, searchParam, currentPage);
        }
        public string DeleteComplaintById(int complaintId)
        {
            return _icomplaintsRepository.DeleteComplaintById(complaintId);
        }
        public List<ComplaintsGraphData> GetComplaintsGraphData(string productName, string companyName)
        {
            return _icomplaintsRepository.GetComplaintsGraphData(productName, companyName);
        }
    }
}
