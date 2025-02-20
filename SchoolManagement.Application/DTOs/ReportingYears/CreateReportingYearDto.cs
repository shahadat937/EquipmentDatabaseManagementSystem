using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ReportingYear
{
    public class CreateReportingYearDto : IReportingYearDto
    {

        public int ReporingYearId { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }

    }
}
