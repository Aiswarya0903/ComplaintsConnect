using Complaints.Models;
using Complaints.Models.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.IBussiness
{
    public interface IComplaintsManager
    {
        string InsertComplaintDetailsAddEdit(CompalintsDataModel modelObject);
        Task<LiveComplaintsModel> GetViewComplaintById(int complaintId);
        ComplaintsInfo GetComplaintById(int complaintId);
        List<ProductInfo> GetDistinctProductsList();
        List<CompanyInfo> GetDistinctCompaniesListForProduct(string productName);
        ComplaintModelData GetComplaintsData(int currentPage,string searchParams,int page);
        ComplaintsInfoData GetDetailsByProductCompany(string product, string company, string searchParam,int page);
        string DeleteComplaintById(int complaintId);
        List<ComplaintsGraphData> GetComplaintsGraphData(string productName, string companyName);
    }
}
