using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Project
{
   public class ProjectBaseSchoolNameDto
    {
        public int ProjectBaseSchoolNameId { get; set; }
        public int? ProjectId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? BaseSchoolName { get; set; }
    }
}
