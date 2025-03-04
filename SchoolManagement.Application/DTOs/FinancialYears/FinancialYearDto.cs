using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FinancialYears
{
    public class FinancialYearDto : IFinancialYearDto
    { 

        public int FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
