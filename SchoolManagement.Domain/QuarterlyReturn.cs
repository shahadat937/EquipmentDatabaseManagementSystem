using SchoolManagement.Doamin;
using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class QuarterlyReturn:BaseDomainEntity
    {
        public int QuarterlyReturnId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? OperationalStatusId { get; set; }
        public int? ReportingMonthId { get; set; }
        public int ReportingYearId { get; set; }
        public int? MenuPosition { get; set; }
        public string? FileUpload { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual OperationalStatus? OperationalStatus { get; set; }
        public virtual ReportingMonth? ReportingMonth { get; set; }
    }
}
