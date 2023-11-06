using System.ComponentModel.DataAnnotations;

namespace Complaints.Models
{
    public class CompalintsModel1
    { 
        public CompalintsModel? jsonData { get;set;}
    }

    public class CompalintsModel
    {
        public Product? Product { get; set; }
        public SubProduct? SubProduct { get; set; }
        public Company? Company { get; set; }
        public Consumers? Consumers { get; set; }
        public States? States { get; set; }
        public SubmissionMethods? SubmissionMethods { get; set; }
        public CompanyResponses? CompanyResponses { get; set; }
        public Issues? Issues { get; set; } 
        public SubIssues? SubIssues { get; set; }   
        public Timeliness? Timeliness { get; set; } 
        public ConsumerConsents? ConsumerConsents { get; set; } 
        public ComplaintsData? Complaints { get; set; }

    }
    
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class SubProduct
    {
        [Key]
        public int SubProductId { get; set; }
        public string? SubProductName { get;set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class Consumers
    {
        [Key]
        public int ConsumersId { get; set; }
        public string? ConsumersName { get; set; }
        public string? Email { get; set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class States
    {
        [Key]
        public int StatesId { get; set; }
        public string? StatesName { get;set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class SubmissionMethods
    {
        [Key]
        public int SubmissionMethodsId { get; set; }
        public string? SubmissionMethodsName { get; set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class CompanyResponses
    {
        [Key]
        public int CompanyResponsesId { get; set; }
        public string? CompanyResponsesName { get; set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class Issues
    {
        [Key]
        public int IssuesId { get; set; }
        public string? IssuesName { get; set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }

    public class SubIssues
    {
        [Key]
        public int SubIssuesId { get; set;}
        public string? SubIssuesName { get; set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class Timeliness
    {
        [Key]
        public int TimelinessId { get; set;}
        public string? TimelinessName { get; set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
    public class ConsumerConsents
    {
        [Key]
        public int ConsumerConsentsId { get; set; }
        public string? ConsumerConsentsName { get;set; }
        public ICollection<ComplaintsData>? Complaints { get; set; }
    }
   
    public class ComplaintsData
    {
        [Key]
        public int ComplaintsId { get; set; }
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        public int SubProductId { get; set; }
        public int ConsumerId { get; set; }
        public int StateId { get; set;}
        public int SubmissionMethodId { get; set;}
        public int CompanyResponseId { get; set; }
        public int IssueId { get;set; }
        public int SubIssueId { get; set; }
        public int TimelinessId { get; set; }
        public int ConsumerConsentId { get; set; }
        public string? ZipCode { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DateSentToCompany { get; set; }
        public string? ComplaintWhatHappened { get; set;}
        public string? Tags { get; set; }
        public bool HasNarrative { get; set; }
        public string? ConsumerDisputed { get; set; }
        
        public Product? Product { get; set; }
        public SubProduct? SubProduct { get; set; }
        public Company? Company { get; set; }
        public Consumers? Consumers { get; set; }
        public States? States { get; set; }
        public SubmissionMethods? submissionMethods { get; set; }
        public CompanyResponses? CompanyResponses { get; set; }
        public Issues? issues { get; set; }
        public SubIssues? subIssues { get; set; }
        public Timeliness? timeliness { get; set; }
        public ConsumerConsents? consumerConsents { get; set; }


    }
}
