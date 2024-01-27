using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class BookType : BaseDomainEntity
    {
        public BookType()
        {
            BookUserManualBRInfos = new HashSet<BookUserManualBRInfo>();
        }

        public int BookTypeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        
        public virtual ICollection<BookUserManualBRInfo> BookUserManualBRInfos { get; set; }
    }
}
