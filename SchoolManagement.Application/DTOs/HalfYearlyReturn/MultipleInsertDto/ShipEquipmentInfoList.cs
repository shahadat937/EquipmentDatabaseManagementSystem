using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.HalfYearlyReturn.MultipleInsertDto
{
    public class ShipEquipmentInfoList
    {
        public int? BaseSchoolNameId { get; set; }
        public int? EquipmentCategoryId { get; set; }
        public int? EqupmentNameId { get; set; }
        public string? EquipmentCategory { get; set; }
        public string? EqupmentName { get; set; }
        public int? BrandId { get; set; }
        public double? Qty { get; set; }
        public string? PowerSupply { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? StateOfEquipment { get; set; }
        public double? HalfYearlyRunningTime { get; set; }
        public double? TotalRunningTime { get; set; }
        public string? HalfYearlyProblem { get; set; }
        public string? PowerSupplyUnit { get; set; }

    }
}
