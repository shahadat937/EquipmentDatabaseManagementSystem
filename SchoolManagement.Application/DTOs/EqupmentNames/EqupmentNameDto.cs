namespace SchoolManagement.Application.DTOs.EqupmentNames
{
    public class EqupmentNameDto : IEqupmentNameDto
    {
        public int EqupmentNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? EquipmentCategoryId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public string? EquipmentCategory { get; set; }
    }
}
