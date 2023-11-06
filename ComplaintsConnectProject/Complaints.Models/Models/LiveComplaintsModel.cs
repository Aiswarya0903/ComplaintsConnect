using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Models.Models
{
    
    
    public class Hit
    {
        public string? _index { get; set; }
        public string? _type { get; set; }
        public string? _id { get; set; }
        public double _score { get; set; }
        public LiveComplaintsModel? _source { get; set; }
        public Total? total { get; set; }
        public double? max_score { get; set; }
        public List<Hit>? hits { get; set; }
    }

    public class Root
    {
        public int? took { get; set; }
        public bool? timed_out { get; set; }
        public Shards? _shards { get; set; }
        public Hit? hits { get; set; }
    }

    public class Shards
    {
        public int? total { get; set; }
        public int? successful { get; set; }
        public int? skipped { get; set; }
        public int? failed { get; set; }
    }

    public class LiveComplaintsModel
    {
        public string? product { get; set; }
        public string? complaint_what_happened { get; set; }
        public DateTime? date_sent_to_company { get; set; }
        public string? issue { get; set; }

        [JsonProperty(":updated_at")]
        public int updated_at { get; set; }
        public string? date_received_formatted { get; set; }
        public string? sub_product { get; set; }
        public string? zip_code { get; set; }
        public object? tags { get; set; }
        public bool? has_narrative { get; set; }
        public string? complaint_id { get; set; }
        public string? date_sent_to_company_formatted { get; set; }
        public string? timely { get; set; }
        public object? consumer_consent_provided { get; set; }
        public string? company_response { get; set; }
        public DateTime? date_indexed { get; set; }
        public string? submitted_via { get; set; }
        public string? date_indexed_formatted { get; set; }
        public string? company { get; set; }
        public DateTime? date_received { get; set; }
        public string? state { get; set; }
        public string? consumer_disputed { get; set; }
        public object? company_public_response { get; set; }
        public string? sub_issue { get; set; }
    }

    public class Total
    {
        public int? value { get; set; }
        public string? relation { get; set; }
    }

}
