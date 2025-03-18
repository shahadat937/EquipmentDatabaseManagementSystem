using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class FinancialYear : BaseDomainEntity
    {
        public FinancialYear()
        {
            Procurements = new HashSet<Procurement>();
            Budgets = new HashSet<Budget>();
            Projects = new HashSet<Project>();
        }
        public int FinancialYearId { get; set; }
        public string FinancialYearName { get; set; } 
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Procurement> Procurements { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }

}

