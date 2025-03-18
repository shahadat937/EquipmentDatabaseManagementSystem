using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class Controlled : BaseDomainEntity
    {
        public Controlled()
        {
            Procurements = new HashSet<Procurement>();
            Budgets = new HashSet<Budget>();
            Projects = new HashSet<Project>();
        }
        public int ControlledId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; } 
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Procurement> Procurements { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
