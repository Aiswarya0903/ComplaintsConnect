using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Models
{
    public class ComplaintModelData
    {
        public List<ComplaintCountsModel> ComplaintModelsData { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }

    public class ComplaintCountsModel
    {
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        public string? ProductName { get; set; }
        public string? CompanyName { get; set; }
        public int NoOfComplaints { get; set; }
    }
}
