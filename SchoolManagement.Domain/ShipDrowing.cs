using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class ShipDrowing:BaseDomainEntity
    {
        public int ShipDrowingId { get; set; }
        public int? AuthorityId { get; set; }
        public int? BaseNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? FileUpload { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public virtual BaseSchoolName? Authority { get; set; }
        public virtual BaseSchoolName? BaseName { get; set; }
        public virtual BaseSchoolName? BaseSchoolName { get; set; }
    }
}
