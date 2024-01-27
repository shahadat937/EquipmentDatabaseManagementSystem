using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class HalfYearlyReturn : BaseDomainEntity
    {
        public HalfYearlyReturn()
        {
            
        }
        public int HalfYearlyReturnId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? EquipmentCategoryId { get; set; }
        public int? EqupmentNameId { get; set; }
        public int? BrandId { get; set; }
        public double? HalfYearlyRunningTime { get; set; }
        public double? TotalRunningTime { get; set; }
        public string? HalfYearlyProblem { get; set; }
        public string? InputPowerSupply { get; set; }
        public string? PowerSupplyUnit { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual EquipmentCategory? EquipmentCategory { get; set; }
        public virtual EqupmentName? EqupmentName { get; set; }
        public virtual Brand? Brand { get; set; }


    }
}
