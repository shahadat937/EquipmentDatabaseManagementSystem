using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.BookUserManualBRInfo
{
    public class CreateBookUserManualBRInfoDto : IBookUserManualBRInfoDto
    {
        public int BookUserManualBRInfoId { get; set; }
        public int? BookTypeId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? BookName { get; set; }
        public string? UploadDocument { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? Document { get; set; }
    }
}
 