using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Procurement
{
    public class ProcurementDataForBarChartDto
    {
        public int? SentAipCount { get; set; }
        public int? AipApprovedCount { get; set; }
        public int? IndentCount { get; set; }
        public int? TenderFloatedCount { get; set; }
        public int? OfferReceivedCount { get; set; }
        public int? CountractSingCount { get; set; }

        public List<string>? SentAipItems { get; set; }
        public List<string>? AipApprovedItems { get; set; }
        public List<string>? IndentItems { get; set; }
        public List<string>? TenderFloatedItem { get; set; }
        public List<string>? OfferEvulationItems { get; set; }
        public List<string>? ContractSingedItems { get; set; }
    }
}
