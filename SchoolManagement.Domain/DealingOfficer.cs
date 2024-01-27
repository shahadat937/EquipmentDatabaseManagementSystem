﻿using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class DealingOfficer : BaseDomainEntity
    {
        public DealingOfficer()
        {
            DailyWorkStates = new HashSet<DailyWorkState>();
        }
        public int DealingOfficerId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; } 
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<DailyWorkState> DailyWorkStates { get; set; }
    }
}
