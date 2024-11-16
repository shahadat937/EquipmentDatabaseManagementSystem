using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.ShipEquipmentInfo
{
    public class ShipEquipmentCountDto
    {
        public string? EquipmentCategory { get; set; }
        public int? OperanationalCount { get; set; }
        public int? NonOperanationalCount { get; set; }
        public int? TotalCount { get; set; }
    }
}
