using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Procurement
{
   public class ProcurementBaseSchoolNameDto
    {
        public int ProcurementBaseSchoolNameId { get; set; }
        public int? ProcurementId { get; set; }
        public int? BaseSchoolNameId { get; set; }
    }
}
