using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Project
{
    public class ProjectBarChartDto
    {
        public int? ForeignTrainingCount { get; set; }
        public int? FatCount { get; set; }
        public int? InstallationCount { get; set; }
        public List<string>? ForeignTrainingItems { get; set; }
        public List<string>? FatItems { get; set; }
        public List<string>? InstallationItems { get; set; }
    }
}
