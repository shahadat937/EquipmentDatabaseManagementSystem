using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Procurement
{
    public class ProcurementTenderOpeningDto
    {
        public int ProcurementTenderOpeningId { get; set; }
        public int? ProcurementId { get; set; }
        public DateTime? TenderOpeningDate { get; set; }
        public int? TenderOpeningCount { get; set; }
    }
}
