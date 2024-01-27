using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class ProcurementType : BaseDomainEntity
    {
        public ProcurementType()
        {
            Procurements = new HashSet<Procurement>();
        }
        public int ProcurementTypeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; } 
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Procurement> Procurements { get; set; }
    }
}
