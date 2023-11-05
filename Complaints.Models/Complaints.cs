using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintId {  get; set; }
        [ForeignKey("ProductId")]
        public int ProductId {  get; set; }
        public string? ConsumerDisputed { get; set; }
        public string?  CompanyResponse { get; set; }
        [ForeignKey("StateId")]
        public int StateId { get; set; }
        public string? Submittedvia { get; set; }
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public string? Issue { get; set; }
        public string? SubIssue { get; set; }
        public string? Timely { get; set; }
        public string? ConsumerConsent { get; set; }
        public string? ZipCode { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateSentToCompany { get; set; }
        public string? ComplaintWhatHappened { get; set; }
        public string? Tags { get; set; }
        public bool? HasNarrative { get; set; }
        public string? SubProduct { get; set; }
        public int ExComplaintId { get; set; }
        public Product? Product { get; set; }
        
        public Company? Company { get; set; }
        
        public State? State { get; set; }
       
    }
}
