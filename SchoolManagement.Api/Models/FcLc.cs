using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class FcLc
    {
        public FcLc()
        {
            Procurements = new HashSet<Procurement>();
        }

        public int FcLcId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Procurement> Procurements { get; set; }
    }
}
