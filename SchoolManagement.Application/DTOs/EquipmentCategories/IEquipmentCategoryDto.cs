namespace SchoolManagement.Application.DTOs.EquipmentCategorys
{
    public interface IEquipmentCategoryDto
    {
        public int EquipmentCategoryId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? GroupNameId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
