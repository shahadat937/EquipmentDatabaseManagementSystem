using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
   public class ProcurementBaseSchoolName
    {
        public int ProcurementBaseSchoolNameId { get; set; }
        public int? ProcurementId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public virtual BaseSchoolName? BaseSchoolName { get; set; } 
        public virtual Procurement? Procurement { get; set; }
    }
}
