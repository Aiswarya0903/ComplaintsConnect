using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Models
{
    public class ComplaintsDataModel
    {
        public ModelObject? ModelObject { get; set; }
    }
    public class ModelObject
    {
        public Product? Product { get; set; }
        public Company? Company { get; set; }
        public State? State { get; set; }
        public Complaint? Complaint { get; set; }

    }
}
