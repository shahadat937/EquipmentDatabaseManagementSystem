using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.YearlyReturns
{
    public class YearlyReturnDto
    {
        public int YearlyReturnId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? OperationalStatusId { get; set; }
        public int? ReportingMonthId { get; set; }
        public int? ReportingYearId { get; set; }
        public int? MenuPosition { get; set; }
        public string? CreatedBy { get; set; }
        public string? SchoolName { get; set; }
        //public string BaseSchoolName { get; set; }
        public string? OperationalStatus { get; set; }
        public string? ReportingMonth { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
