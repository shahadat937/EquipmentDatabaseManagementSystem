using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.YearSetup
{
    public class YearSetupDto : IYearSetupDto
    {


        public int YearId { get; set; }
        public int Year { get; set; }
        public string? IsActive { get; set; }
    }
}
