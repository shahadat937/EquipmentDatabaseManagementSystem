using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class ProcurementMethod : BaseDomainEntity
    {
        public ProcurementMethod()
        {
            //Procurements = new HashSet<Procurement>();
        }
        public int ProcurementMethodId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<Procurement> Procurements { get; set; }
    }
}
