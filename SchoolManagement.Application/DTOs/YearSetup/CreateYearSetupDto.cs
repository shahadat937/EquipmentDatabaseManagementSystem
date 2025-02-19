using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.YearSetup
{
    public class CreateYearSetupDto : IYearSetupDto
    {
       
        public int Year { get; set; }
        
    }
}
