

namespace SchoolManagement.Application.DTOs.Budget
{
    public class BudgetDto : IBudgetDto
    {
        public int BudgetId { get; set; }
        public int? EqupmentNameId { get; set; }
        public double? Qty { get; set; }
        public int? DgdpNssdId { get; set; }
        public decimal? ContractualPriceLC { get; set; }
        public decimal? ContractualPriceFC { get; set; }
        public int? BudgetCodeId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ControlledId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? FAT { get; set; }
        public string? ForeignTraining { get; set; }
        public decimal? AllocatedBudget { get; set; }
        public DateTime? AllocatedBudgetDate { get; set; }
        public decimal? RevisedAllocatedBudget { get; set; }
        public DateTime? RevisedAllocatedBudgetDate { get; set; }
        public decimal? BillPainDNWEEFC { get; set; }
        public decimal? BillPainDNWEELC { get; set; }
        public string? BillPaidOther { get; set; }
        public int? TermsOfPayment { get; set; }
        public string? Remarks { get; set; }
        public List<int?> BaseSchoolNameId { get; set; }
        public  string? DgdpNssdName { get; set; }
        public  string? ControlledName { get; set; }
        public  string? EqupmentName { get; set; }
        public  string? BudgetCodeName { get; set; }
        public  string? BudgetCode { get; set; }
        public  string? FinancialYearName { get; set; }


        public List<BudgetBaseSchoolNameDto>?  BaseSchoolNameDtos { get; set; }


    }
}
