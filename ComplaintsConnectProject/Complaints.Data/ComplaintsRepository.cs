using Complaints.Data.Models;
using Complaints.IData;
using Complaints.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Complaints.Models.Models;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Complaints.Data
{
    public class ComplaintsRepository : IComplaintsRepository
    {
        ComplaintsDbContext _complaintsDbContext;
        string _constr = string.Empty;
        //SqlDatabase db;


        public ComplaintsRepository(string Connstr)
        {
            _complaintsDbContext = new ComplaintsDbContext(Connstr);
            _constr = Connstr;
            //db = new SqlDatabase(_constr);
        }

        public List<ProductInfo> GetDistinctProductsList()
        {
            var df = from p in _complaintsDbContext.Product
                     group p by p.ProductName into g
                     orderby g.Key ascending
                     select new ProductInfo
                     {
                         ProductName = g.Key
                     };

            return df.ToList();
        }
        public ComplaintsInfo GetComplaintById(int complaintId)
        {
            var query = from c in _complaintsDbContext.Complaint
                        join P in _complaintsDbContext.Product on c.ProductId equals P.ProductId
                        join cc in _complaintsDbContext.Company on P.CompanyId equals cc.CompanyId
                        where c.ComplaintId == complaintId
                        select new ComplaintsInfo
                        {
                            ComplaintId = c.ComplaintId,
                            ProductId = P.ProductId,
                            ProductName = P.ProductName,
                            ConsumerDisputed = c.ConsumerDisputed,
                            CompanyResponse = c.CompanyResponse,
                            StateId = c.StateId,
                            Submittedvia = c.Submittedvia,
                            CompanyId = cc.CompanyId,
                            CompanyName = cc.CompanyName,
                            Issue = c.Issue,
                            SubIssue = c.SubIssue,
                            Timely = c.Timely,
                            ConsumerConsent = c.ConsumerConsent,
                            ZipCode = c.ZipCode,
                            DateReceived = c.DateReceived,
                            DateSentToCompany = c.DateSentToCompany,
                            ComplaintWhatHappened = c.ComplaintWhatHappened,
                            Tags = c.Tags,
                            HasNarrative = c.HasNarrative!=null?(bool)c.HasNarrative:false,
                            SubProduct = c.SubProduct
                        };
            return query.FirstOrDefault();
        }
        public List<CompanyInfo> GetDistinctCompaniesListForProduct(string productName)
        {
            var query = from product in _complaintsDbContext.Product
                        join company in _complaintsDbContext.Company
                            on product.CompanyId equals company.CompanyId
                        where product.ProductName == productName
                        orderby company.CompanyName ascending
                        select new CompanyInfo
                        {
                            CompanyId = company.CompanyId,
                            CompanyName = company.CompanyName
                        };

            return query.ToList();
        }

        public string InsertComplaintDetailsAddEdit(CompalintsDataModel modelObject)
        {
            string message = string.Empty;
            if (modelObject != null && modelObject.ModelObject != null &&
            modelObject.ModelObject.Complaint != null
            && modelObject.ModelObject.Complaint.ComplaintId <= 0)
            {
                var company = new Company();
                var product = new Product();
                
                if (modelObject.ModelObject.Company.CompanyId<=0)
                {
                    company = new Company
                    {
                        CompanyName = modelObject.ModelObject.Company.CompanyName
                    };
                    _complaintsDbContext.Company.Add(company);
                    _complaintsDbContext.SaveChanges();

                    product = new Product
                    {
                        ProductName = modelObject.ModelObject.Product.ProductName,
                        CompanyId=company.CompanyId
                        
                    };
                    _complaintsDbContext.Product.Add(product);
                    _complaintsDbContext.SaveChanges();
                }
                
                   var query = _complaintsDbContext.Product
                    .Where(product => product.ProductName == modelObject.ModelObject.Product.ProductName && product.CompanyId == Convert.ToInt32(modelObject.ModelObject.Company.CompanyId==0? company.CompanyId: modelObject.ModelObject.Company.CompanyId))
                    .Select(product => new
                    {
                        product.ProductName,
                        product.ProductId,
                        product.CompanyId
                    })
                    .SingleOrDefault();
                




                var complaints = new Complaint
                {
                    ProductId = query.ProductId,
                    ConsumerDisputed = modelObject.ModelObject?.Complaint?.ConsumerDisputed,
                    CompanyResponse = modelObject.ModelObject?.Complaint?.CompanyResponse,
                    StateId = Convert.ToInt32(modelObject.ModelObject.State.StateName),
                    Submittedvia = modelObject.ModelObject?.Complaint?.Submittedvia,
                    CompanyId = query.CompanyId,
                    Issue = modelObject.ModelObject?.Complaint?.Issue,
                    SubIssue = modelObject.ModelObject?.Complaint?.SubIssue,
                    Timely = modelObject.ModelObject?.Complaint?.Timely,
                    ConsumerConsent = modelObject.ModelObject?.Complaint?.ConsumerConsent,
                    ZipCode = modelObject.ModelObject?.Complaint?.ZipCode,
                    DateReceived = (DateTime)(modelObject.ModelObject?.Complaint?.DateReceived),
                    DateSentToCompany = (DateTime)(modelObject.ModelObject?.Complaint?.DateSentToCompany),
                    ComplaintWhatHappened = modelObject.ModelObject?.Complaint?.ComplaintWhatHappened,
                    Tags = modelObject.ModelObject?.Complaint?.Tags,
                    HasNarrative = (bool)(modelObject.ModelObject?.Complaint?.HasNarrative),
                    SubProduct = modelObject.ModelObject?.Complaint?.SubProduct,



                };
                _complaintsDbContext.Add(complaints);
                _complaintsDbContext.SaveChanges();
                message = "Compalint Inserted Successfully?complaintsId="+complaints.ComplaintId.ToString();
            }
            else
            {

                var query = _complaintsDbContext.Complaint
                .Where(x => x.ComplaintId == modelObject.ModelObject.Complaint.ComplaintId)
                .Select(x => x).SingleOrDefault();

                if (query != null)
                {

                    query.ConsumerDisputed = modelObject?.ModelObject?.Complaint?.ConsumerDisputed;
                    query.CompanyResponse = modelObject?.ModelObject?.Complaint?.CompanyResponse;
                    //query.StateId = modelObject.ModelObject.Complaint.StateId;
                    query.Submittedvia = modelObject?.ModelObject?.Complaint?.Submittedvia;
                    query.Issue = modelObject?.ModelObject?.Complaint?.Issue;
                    query.SubIssue = modelObject?.ModelObject?.Complaint?.SubIssue;
                    query.Timely = modelObject?.ModelObject?.Complaint?.Timely;
                    query.ConsumerConsent = modelObject?.ModelObject?.Complaint?.ConsumerConsent;
                    query.ZipCode = modelObject?.ModelObject?.Complaint?.ZipCode;
                    query.DateReceived = modelObject.ModelObject.Complaint.DateReceived;
                    query.DateSentToCompany = modelObject.ModelObject.Complaint.DateSentToCompany;
                    query.ComplaintWhatHappened = modelObject?.ModelObject?.Complaint?.ComplaintWhatHappened;
                    query.Tags = modelObject?.ModelObject?.Complaint?.Tags;
                    query.HasNarrative = modelObject?.ModelObject?.Complaint?.HasNarrative;
                    query.SubProduct = modelObject?.ModelObject?.Complaint?.SubProduct;

                    _complaintsDbContext.Update(query);
                    _complaintsDbContext.SaveChanges();
                    message = "Compalint Update Successfully.";
                }


            }
            return message;
        }
        public ComplaintModelData GetComplaintsData(int currentPage, string? searchParams, int page)
        {
            var ComplaintModelData = new ComplaintModelData();

            int pageSize = 10; // Number of records per page
            int pageNumber = page; // Page number (1-based index)

            int number;
            int? complaintId = null;

            if (int.TryParse(searchParams, out number))
            {
                searchParams = "";
                complaintId = number;
            }
            else
            {
                number = 0;
                complaintId = number;

            }

            //Sarath
            IQueryable<ComplaintCountsModel> query;
            if (complaintId==0)
            {
                 query = (from complaint in _complaintsDbContext.Complaint
                             join product in _complaintsDbContext.Product on complaint.ProductId equals product.ProductId
                             join company in _complaintsDbContext.Company on product.CompanyId equals company.CompanyId
                             where (product.ProductName != null && product.ProductName.StartsWith(searchParams??""))
                                   || (company.CompanyName != null && company.CompanyName.StartsWith(searchParams?? ""))
                             group new { product, company } by new { product.ProductId, product.CompanyId } into grouped
                             select new ComplaintCountsModel
                             {
                                 ProductId = grouped.Key.ProductId,
                                 CompanyId = grouped.Key.CompanyId,
                                 ProductName = grouped.Max(item => item.product.ProductName),
                                 CompanyName = grouped.Max(item => item.company.CompanyName),
                                 NoOfComplaints = grouped.Count(),
                             });

            }
            else
            {
                query = (from complaint in _complaintsDbContext.Complaint
                         join product in _complaintsDbContext.Product on complaint.ProductId equals product.ProductId
                         join company in _complaintsDbContext.Company on product.CompanyId equals company.CompanyId
                         where
                         (
                            //string.IsNullOrEmpty(searchParams) ||
                            //(complaint.Product.ProductName ?? "").Contains(searchParams.Trim()) ||
                            //(complaint.Company.CompanyName ?? "").Contains(searchParams.Trim()) ||
                            !complaintId.HasValue || complaint.ComplaintId == Convert.ToInt64(complaintId)
                         )
                         group new { product, company } by new { product.ProductId, product.CompanyId } into grouped
                         select new ComplaintCountsModel
                         {
                             ProductId = grouped.Key.ProductId,
                             CompanyId = grouped.Key.CompanyId,
                             ProductName = grouped.Max(item => item.product.ProductName),
                             CompanyName = grouped.Max(item => item.company.CompanyName),
                             NoOfComplaints = grouped.Count(),
                         });
            }

            _complaintsDbContext.Database.SetCommandTimeout(180);
            _complaintsDbContext.Database.CloseConnection();
            var result = query.Skip((pageNumber - 1) * pageSize) // Skip records on previous pages
                              .Take(pageSize); // Take the current page of records
            var recodlist = result.ToList();
            var totalcount = query.Count();// _complaintsDbContext.Complaint.Count();

            ComplaintModelData.TotalRecords = totalcount;
            ComplaintModelData.CurrentPage = pageNumber;
            ComplaintModelData.TotalPages = (int)Math.Ceiling((double)totalcount / pageSize);
            ComplaintModelData.ComplaintModelsData = recodlist;
            return ComplaintModelData;


            //var ComplaintModelData = new ComplaintModelData();

            //int pageSize = 10; // Number of records per page
            //int pageNumber = page; // Page number (1-based index)

            //int number;
            //int? complaintId = null;

            //if (int.TryParse(searchParams,out number))
            //{
            //    searchParams = "12345";
            //    complaintId = number;
            //}
            //else
            //{
            //    number = 0;
            //    complaintId = number;

            //}

            //var query = (from complaint in _complaintsDbContext.Complaint
            //             join product in _complaintsDbContext.Product on complaint.ProductId equals product.ProductId
            //             join company in _complaintsDbContext.Company on product.CompanyId equals company.CompanyId
            //             where
            //             (
            //                string.IsNullOrEmpty(searchParams) ||
            //                complaint.Product.ProductName.Contains(searchParams.Trim()) ||
            //                complaint.Company.CompanyName.Contains(searchParams.Trim()) ||
            //                !complaintId.HasValue || complaint.ComplaintId == Convert.ToInt64(complaintId)
            //             )
            //             group new { product, company } by new { product.ProductId, product.CompanyId } into grouped
            //             select new ComplaintCountsModel
            //             {
            //                 ProductId = grouped.Key.ProductId,
            //                 CompanyId = grouped.Key.CompanyId,
            //                 ProductName = grouped.Max(item => item.product.ProductName),
            //                 CompanyName = grouped.Max(item => item.company.CompanyName),
            //                 NoOfComplaints = grouped.Count(),
            //             }).Take(100).ToList();

            //var result = query.Skip((pageNumber - 1) * pageSize) // Skip records on previous pages
            //                  .Take(pageSize); // Take the current page of records

            //ComplaintModelData.TotalRecords = 4237626;
            //ComplaintModelData.CurrentPage = pageNumber;
            //ComplaintModelData.TotalPages = (int)Math.Ceiling((double)query.Count() / pageSize);
            //ComplaintModelData.ComplaintModelsData = result.ToList();

            //return ComplaintModelData;



        }
        public ComplaintsInfoData GetDetailsByProductCompany(string product, string company, string? searchParam, int currentPage)
        {
            //var ComplaintsInfoData = new ComplaintsInfoData();

            //int pageSize = 10; // Number of records per page
            //int pageNumber = currentPage; // Page number (1-based index)

            //int number;
            //int? complaintId = null;

            //if (int.TryParse(searchParam, out number))
            //{
            //    searchParam = "12345";
            //    complaintId = number;
            //}
            //else
            //{
            //    number = 0;
            //    complaintId = number;
            //}

            //var query = (from c in _complaintsDbContext.Complaint
            //             join P in _complaintsDbContext.Product on c.ProductId equals P.ProductId
            //             join cc in _complaintsDbContext.Company on P.CompanyId equals cc.CompanyId
            //             where
            //             (
            //              P.ProductName == product && cc.CompanyName == company &&
            //                string.IsNullOrEmpty(searchParam) &&
            //                c.Product.ProductName.Contains(searchParam.Trim()) ||
            //                c.Company.CompanyName.Contains(searchParam.Trim()) &&
            //                !complaintId.HasValue || c.ComplaintId == Convert.ToInt64(complaintId)
            //             ) 
            //             select new ComplaintsInfo
            //             {
            //                 ComplaintId = c.ComplaintId,
            //                 ProductId = P.ProductId,
            //                 ProductName = P.ProductName,
            //                 ConsumerDisputed = c.ConsumerDisputed,
            //                 CompanyResponse = c.CompanyResponse,
            //                 StateId = c.StateId,
            //                 Submittedvia = c.Submittedvia,
            //                 CompanyId = cc.CompanyId,
            //                 CompanyName = cc.CompanyName,
            //                 Issue = c.Issue,
            //                 SubIssue = c.SubIssue,
            //                 Timely = c.Timely,
            //                 ConsumerConsent = c.ConsumerConsent,
            //                 ZipCode = c.ZipCode,
            //                 DateReceived = c.DateReceived,
            //                 DateSentToCompany = c.DateSentToCompany,
            //                 ComplaintWhatHappened = c.ComplaintWhatHappened,
            //                 Tags = c.Tags,
            //                 HasNarrative = c.HasNarrative,
            //                 SubProduct = c.SubProduct
            //             }).ToList();
            //// return query.ToList();

            //var result = query.Skip((pageNumber - 1) * pageSize) // Skip records on previous pages
            //                .Take(pageSize); // Take the current page of records

            //var recodlist = result.ToList();
            //var totalcount = query.Count();

            //ComplaintsInfoData.TotalRecords = totalcount;
            //ComplaintsInfoData.CurrentPage = pageNumber;
            //ComplaintsInfoData.TotalPages = (int)Math.Ceiling((double)totalcount / pageSize);
            //ComplaintsInfoData.ComplaintInfoData = recodlist;

            //return ComplaintsInfoData;
            var ComplaintsInfoData = new ComplaintsInfoData();

            int pageSize = 10; // Number of records per page
            int pageNumber = currentPage; // Page number (1-based index)

            int number;
            int? complaintId = null;

            if (int.TryParse(searchParam, out number))
            {
                searchParam = "12345";
                complaintId = number;
            }
            else
            {
                number = 0;
                complaintId = number;
            }

            var query = (from c in _complaintsDbContext.Complaint
                         join P in _complaintsDbContext.Product on c.ProductId equals P.ProductId
                         join cc in _complaintsDbContext.Company on P.CompanyId equals cc.CompanyId
                         //where P.ProductName == (product ?? "") && cc.CompanyName == (company ?? "")
                         where
                         (
                            P.ProductName == (product ?? "") && cc.CompanyName == (company ?? "") &&
                            string.IsNullOrEmpty(searchParam) &&
                            (P.ProductName ?? "").Contains(searchParam ?? "".Trim()) ||
                            (cc.CompanyName ?? "").Contains(searchParam ?? "".Trim()) &&
                            !complaintId.HasValue || c.ComplaintId == Convert.ToInt64(complaintId)
                         )
                         select new ComplaintsInfo
                         {
                             ComplaintId = c.ComplaintId,
                             ProductId = P.ProductId,
                             ProductName = P.ProductName,
                             ConsumerDisputed = c.ConsumerDisputed ?? "",
                             CompanyResponse = c.CompanyResponse ?? "",
                             StateId = c.StateId,
                             Submittedvia = c.Submittedvia,
                             CompanyId = cc.CompanyId,
                             CompanyName = cc.CompanyName,
                             Issue = c.Issue,
                             SubIssue = c.SubIssue,
                             Timely = c.Timely,
                             ConsumerConsent = c.ConsumerConsent,
                             ZipCode = c.ZipCode ?? "",
                             DateReceived = c.DateReceived,
                             DateSentToCompany = c.DateSentToCompany,
                             ComplaintWhatHappened = c.ComplaintWhatHappened ?? "",
                             Tags = c.Tags ?? "",
                             HasNarrative = c.HasNarrative == null ? false : true,
                             SubProduct = c.SubProduct ?? ""
                         });
            // return query.ToList();
            //Sarath

            _complaintsDbContext.Database.SetCommandTimeout(180);
            _complaintsDbContext.Database.CloseConnection();
            var result = query.Skip((pageNumber - 1) * pageSize) // Skip records on previous pages
                            .Take(pageSize); // Take the current page of records

            var recodlist = result.ToList();
            var totalcount = query.Count();

            ComplaintsInfoData.TotalRecords = totalcount;
            ComplaintsInfoData.CurrentPage = pageNumber;
            ComplaintsInfoData.TotalPages = (int)Math.Ceiling((double)totalcount / pageSize);
            ComplaintsInfoData.ComplaintInfoData = recodlist;

            return ComplaintsInfoData;
        }
        public string DeleteComplaintById(int complaintId)
        {
            string outputResponse = "";

            try
            {
                var query = _complaintsDbContext.Complaint
                                .Where(x => x.ComplaintId == complaintId)
                                .Select(x => x).SingleOrDefault();

                if (query != null)
                {
                    _complaintsDbContext.Remove(query);
                    _complaintsDbContext.SaveChanges();
                }

                outputResponse = "Deleted Successfully";
            }
            catch (Exception ex)
            {

                outputResponse = "Failed to Delete, Please try later.";
            }

            return outputResponse;
        }
        public List<ComplaintsGraphData> GetComplaintsGraphData(string productName, string companyName)
        {
            var query = from complaint in _complaintsDbContext.Complaint
                        join company in _complaintsDbContext.Company
                            on complaint.CompanyId equals company.CompanyId
                        join product in _complaintsDbContext.Product
                            on complaint.ProductId equals product.ProductId
                        where product.ProductName == productName
                            && company.CompanyName == companyName
                        group complaint by complaint.DateReceived.Year into g
                        select new ComplaintsGraphData
                        {
                            Year = g.Key,
                            NoOfComplaints = g.Count()
                        };
            _complaintsDbContext.Database.SetCommandTimeout(180);
            _complaintsDbContext.Database.CloseConnection();
            return query.ToList();
        }
    }
}
