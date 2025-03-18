using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class BudgetCode : BaseDomainEntity
    {
        public BudgetCode()
        {
            Budgets = new HashSet<Budget>();
            Projects = new HashSet<Project>();
        }
        public int BudgetCodeId { get; set; }
        public string BudgetCodeName { get; set; }
        public string Code { get; set; } 
        public string? Remarks { get; set; }    
        public bool IsActive { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }

}
