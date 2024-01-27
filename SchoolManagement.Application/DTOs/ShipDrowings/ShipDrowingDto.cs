using SchoolManagement.Application.DTOs.ShipDrowinges;

namespace SchoolManagement.Application.DTOs.ShipDrowings
{
    public class ShipDrowingDto : IShipDrowingDto
    {
        public int ShipDrowingId { get; set; }
        public int? AuthorityId { get; set; }
        public int? BaseNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? FileUpload { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? Authority { get; set; } 
        public string? BaseName { get; set; }
        public string? BaseSchoolName { get; set; }
    }
}
