using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public class FinancialYears : BaseDomainEntity
    {

        public int FinancialYearId { get; set; }

        public string FinancialYearName { get; set; } 
        public string? Remark { get; set; }
        public bool IsActive { get; set; }

    }

}

