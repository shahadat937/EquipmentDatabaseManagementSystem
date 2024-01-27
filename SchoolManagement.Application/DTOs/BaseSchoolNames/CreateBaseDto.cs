using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs.Common;

namespace SchoolManagement.Application.DTOs.BaseSchoolNames
{
    public class CreateBaseDto
    {
        public IFormFile file { get; set; }
        public CreateBaseSchoolNameDto BaseSchoolNameForm { get; set; }
    }
}
