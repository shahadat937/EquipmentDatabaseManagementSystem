using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
   public class ProjectBaseSchoolName
    {
        public int ProjectBaseSchoolNameId { get; set; }
        public int? ProjectId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public virtual BaseSchoolName? BaseSchoolName { get; set; } 
        public virtual Project? Project { get; set; }
    }
}
