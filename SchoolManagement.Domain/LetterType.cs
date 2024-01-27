using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class LetterType:BaseDomainEntity
    {
        public LetterType()
        {
            DailyWorkStates = new HashSet<DailyWorkState>();
        }
        public int LetterTypeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public string? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<DailyWorkState> DailyWorkStates { get; set; }
    }
}
