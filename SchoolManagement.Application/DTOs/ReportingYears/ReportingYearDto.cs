using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ReportingYear
{
    public class ReportingYearDto : IReportingYearDto
    {


        public int ReportingYearId { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
    }
}
