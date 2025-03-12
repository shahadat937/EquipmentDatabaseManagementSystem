using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class Budget : BaseDomainEntity
    {

        public Budget()
        {
            BudgetBaseSchoolNames = new HashSet<BudgetBaseSchoolName>();
            
        }
        public int BudgetId { get; set; }
        public int? EqupmentNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
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
        public virtual EqupmentName? EqupmentName { get; set; }
        public virtual Controlled? Controlled { get; set; }
        public virtual DgdpNssd? DgdpNssd { get; set; }
        public virtual FinancialYear? FinancialYear { get; set; }
        public virtual BudgetCode? BudgetCode { get; set; }


        public virtual ICollection<BudgetBaseSchoolName>? BudgetBaseSchoolNames { get; set; }
    }
}
