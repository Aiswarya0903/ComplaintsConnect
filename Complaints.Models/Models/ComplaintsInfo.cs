using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Models.Models
{
    public class ComplaintsInfoData
    {
        public List<ComplaintsInfo> ComplaintInfoData { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
    public class ComplaintsInfo
    {
        public int ComplaintId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ConsumerDisputed { get; set; }
        public string? CompanyResponse { get; set; }
        public int StateId { get; set; }
        public string? StateName { get; set; }
        public string? Submittedvia { get; set; }
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? Issue { get; set; }
        public string? SubIssue { get; set; }
        public string? Timely { get; set; }
        public string? ConsumerConsent { get; set; }
        public string? ZipCode { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateSentToCompany { get; set; }
        public string? ComplaintWhatHappened { get; set; }
        public string? Tags { get; set; }
        public bool HasNarrative { get; set; }
        public string? SubProduct { get; set; }
    }
}
