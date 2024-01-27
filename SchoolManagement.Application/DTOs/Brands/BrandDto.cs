namespace SchoolManagement.Application.DTOs.Brands
{
    public class BrandDto : IBrandDto
    {
        public int BrandId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

    }
}
