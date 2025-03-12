using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Budget
{
   public class BudgetBaseSchoolNameDto
    {
        public int BudgetBaseSchoolNameId { get; set; }
        public int? BudgetId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? BaseSchoolName { get; set; }
    }
}
