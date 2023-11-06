using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Models
{
    public class Product
    {
        [Key] // Primary key
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public Company? Company { get; set; } // Navigation property
    }
}
