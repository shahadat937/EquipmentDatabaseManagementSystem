using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.QuarterlyReturns
{
    public class QuarterlyReturnDto : IQuarterlyReturnDto
    {
        public int QuarterlyReturnId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? FileUpload { get; set; }
        public int? OperationalStatusId { get; set; }
        public int? ReportingMonthId { get; set; }
        public int? ReportingYearId { get; set; }
        public int? ReportingYear { get; set; }
        public int? MenuPosition { get; set; }
        public string? SchoolName { get; set; }
        public string? OperationalStatus { get; set; }
        public string? ReportingMonth { get; set; }
        public bool IsActive { get; set; }
    }
}
