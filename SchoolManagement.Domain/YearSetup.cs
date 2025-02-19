using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain
{
    public class YearSetup : BaseDomainEntity
    {
        public int YearId { get; set; }
        public int Year { get; set; }
        public string?  IsActive { get; set; }
    }
}
