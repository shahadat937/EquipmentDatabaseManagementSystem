using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class BookUserManualBRInfo : BaseDomainEntity
    {
        public BookUserManualBRInfo()
        {
         
        }

        public int BookUserManualBRInfoId { get; set; }
        public int? BookTypeId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? BookName { get; set; }
        public string? UploadDocument { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual BookType? BookType { get; set; }
    }
}
