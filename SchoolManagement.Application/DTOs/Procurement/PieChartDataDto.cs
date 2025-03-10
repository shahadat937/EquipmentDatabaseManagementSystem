using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Procurement
{
    public class PieChartDataDto
    {
        public string? FinancialYear  { get; set; }
        public int? TotalProcurementCount { get; set; }
        public int? TotalContractSignedProcurementCount { get; set; }

    }
}
