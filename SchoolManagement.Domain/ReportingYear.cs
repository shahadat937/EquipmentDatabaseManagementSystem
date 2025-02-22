using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class ReportingYear : BaseDomainEntity
    {
        public ReportingYear()
        {
            QuarterlyReturns = new HashSet<QuarterlyReturn>();
            YearlyReturns = new HashSet<YearlyReturn>();
        }
        public int ReportingYearId { get; set; }
        public int Year { get; set; }
        public bool  IsActive { get; set; }
        public virtual ICollection<QuarterlyReturn> QuarterlyReturns { get; set; }
        public virtual ICollection<YearlyReturn> YearlyReturns { get; set; }
    }
}
