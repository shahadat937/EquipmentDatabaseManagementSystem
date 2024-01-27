using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.ShipDrowinges
{
    public class CreateShipDrowingDto : IShipDrowingDto
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
        public IFormFile? Doc { get; set; }
  }
}
