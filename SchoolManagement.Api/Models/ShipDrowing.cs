using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class ShipDrowing
    {
        public int ShipDrowingId { get; set; }
        public int? AuthorityId { get; set; }
        public int? BaseNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string FileUpload { get; set; }
        public string Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName Authority { get; set; }
        public virtual BaseSchoolName BaseName { get; set; }
        public virtual BaseSchoolName BaseSchoolName { get; set; }
    }
}
