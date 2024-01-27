namespace SchoolManagement.Application.DTOs.OperationalStatuss
{
    public class CreateOperationalStatusDto : IOperationalStatusDto
    {
        public int OperationalStatusId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 