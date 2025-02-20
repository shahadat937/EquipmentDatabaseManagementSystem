using SchoolManagement.Domain;
using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Doamin
{
    public class ReportingMonth:BaseDomainEntity
    {
        public ReportingMonth() 
        {
            MonthlyReturns = new HashSet<MonthlyReturn>();
            QuarterlyReturns = new HashSet<QuarterlyReturn>();
        }

        public int ReportingMonthId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }
        public virtual ICollection<HalfYearlyReturn> HalfYearlyReturns { get; set; }
        public virtual ICollection<QuarterlyReturn> QuarterlyReturns { get; set; }
    }
}
