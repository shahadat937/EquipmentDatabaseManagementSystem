using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs.DailyWorkState;

namespace SchoolManagement.Application.DTOs.DailyWorkState
{
    public class CreateInitialiseDto
    {
        public IFormFile Document { get; set; }
        public CreateDailyWorkStateDto DailyWorkStateForm { get; set; }
    }
}
