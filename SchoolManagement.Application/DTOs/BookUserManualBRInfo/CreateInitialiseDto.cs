using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;

namespace SchoolManagement.Application.DTOs.BookUserManualBRInfo
{
    public class CreateInitialiseDto
    {
        public IFormFile Document { get; set; }
        public CreateBookUserManualBRInfoDto BookUserManualBRInfoForm { get; set; }
    }
}
