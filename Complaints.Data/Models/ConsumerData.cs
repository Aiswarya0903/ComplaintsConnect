using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Data.Models
{
    public partial class ConsumerData
    {
        [Key]
        public int ConsumerId { get; set; }
        public string? product { get; set; }


        public string? complaint_what_happened { get; set; }


        public DateTime date_sent_to_company { get; set; }


        public string? issue { get; set; }


        public string? sub_product { get; set; }



        public string? zip_code { get; set; }


        public string? tags { get; set; }


        public bool has_narrative { get; set; }



        public string? complaint_id { get; set; }


        public string? timely { get; set; }


        public string? consumer_consent_provided { get; set; }


        public string? company_response { get; set; }


        public string? submitted_via { get; set; }


        public string? company { get; set; }


        public DateTime date_received { get; set; }


        public string? state { get; set; }


        public string? consumer_disputed { get; set; }


        public string? company_public_response { get; set; }


        public string? sub_issue { get; set; }


    }
}
