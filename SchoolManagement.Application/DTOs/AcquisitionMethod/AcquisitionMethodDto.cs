namespace SchoolManagement.Application.DTOs.AcquisitionMethod
{
    public class AcquisitionMethodDto : IAcquisitionMethodDto
    {
        public int AcquisitionMethodId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

    }
}
