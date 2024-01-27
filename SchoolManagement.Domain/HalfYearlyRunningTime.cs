using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class HalfYearlyRunningTime : BaseDomainEntity
    {
        public HalfYearlyRunningTime()
        {
           
        }
        public int HalfYearlyRunningTimeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        
    }
}
