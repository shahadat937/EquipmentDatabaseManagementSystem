namespace SchoolManagement.Application.DTOs.HalfYearlyReturn
{
    public class HalfYearlyReturnDto : IHalfYearlyReturnDto
    {
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

        public string? EquipmentCategory { get; set; }
        public string? EqupmentName { get; set; }
    }
}
