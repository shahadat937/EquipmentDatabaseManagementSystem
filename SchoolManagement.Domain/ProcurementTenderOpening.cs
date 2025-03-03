using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
   public class ProcurementTenderOpening
    {
        public int ProcurementTenderOpeningId { get; set; }
        public int? ProcurementId { get; set; }
        public DateTime? TenderOpeningDate { get; set; }
        public string? TenderOpeningCount { get; set; }
    }
}
