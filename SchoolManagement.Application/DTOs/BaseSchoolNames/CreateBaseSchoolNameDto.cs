using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.BaseSchoolNames 
{
    public class CreateBaseSchoolNameDto : IBaseSchoolNameDto
    {
        public int BaseSchoolNameId { get; set; }
        public int? BaseNameId { get; set; }
        public string? SchoolName { get; set; }
        public string? ShortName { get; set; }
        public string? SchoolLogo { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public int? BranchLevel { get; set; }
        public int? FirstLevel { get; set; }
        public int? SecondLevel { get; set; }
        public int? ThirdLevel { get; set; }
        public int? FourthLevel { get; set; }
        public int? FifthLevel { get; set; }
        public string? ServerName { get; set; }

        public IFormFile? Image { get; set; }
    }
}
