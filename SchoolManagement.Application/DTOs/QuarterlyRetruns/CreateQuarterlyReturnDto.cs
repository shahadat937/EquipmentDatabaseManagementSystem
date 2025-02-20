using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.QuarterlyReturns
{
    public class CreateQuarterlyReturnDto:IQuarterlyReturnDto
    {
        public int QuarterlyReturnId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? OperationalStatusId { get; set; }
        public string?  FileUpload { get; set; }
        public int? ReportingMonthId { get; set; }
        public int? ReportingYearId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? Doc { get; set; }
    }
}
