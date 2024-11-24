using SchoolManagement.Doamin;
using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class YearlyReturn:BaseDomainEntity
    {
        public int YearlyReturnId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? OperationalStatusId { get; set; }
        public int? ReportingMonthId { get; set; }
        public int ReportingYearId { get; set; }
        public int? MenuPosition { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual OperationalStatus? OperationalStatus { get; set; }
        public virtual ReportingMonth? ReportingMonth { get; set; }
    }
}
