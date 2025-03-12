using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
   public class BudgetBaseSchoolName
    {
        public int BudgetBaseSchoolNameId { get; set; }
        public int? BudgetId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public virtual BaseSchoolName? BaseSchoolName { get; set; } 
        public virtual Budget? Project { get; set; }
    }
}
