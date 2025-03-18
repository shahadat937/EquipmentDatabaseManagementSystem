using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class Project : BaseDomainEntity
    {

        public Project()
        {
            ProjectBaseSchoolNames = new HashSet<ProjectBaseSchoolName>();
        }
        public int ProjectId { get; set; }
        public int? EqupmentNameId { get; set; }
        public double? Qty { get; set; }
        public int? DgdpNssdId { get; set; }
        public decimal? EPrice { get; set; }
        public int? FcLcId { get; set; }
        public int? GroupNameId { get; set; }
        public int? BudgetCodeId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ControlledId { get; set; }
        public DateTime? ContractSigningDate { get; set; }
        public DateTime? ForeignTrainingScheduleAndContractReceived { get; set; }
        public DateTime? ForeignTrainingApprovalForOfficerNomination { get; set; }
        public DateTime? ForeignTrainingGoApproval { get; set; }
        public DateTime? ForeignTraningDate { get; set; }
        public string? ForeignTrainingRemark { get; set; }
        public DateTime? FatSchedule { get; set; }
        public DateTime? FatAcceptance { get; set; }
        public DateTime? FatApprovalForOfficerNomination { get; set; }
        public DateTime? FatGoApproval { get; set; }
        public DateTime? FatDate { get; set; }
        public string? FatRemarks { get; set; }
        public DateTime? ShipmentAndDelivery { get; set; }
        public DateTime? STW { get; set; }
        public DateTime? HAT { get; set; }
        public DateTime? SAT { get; set; }
        public DateTime? LocalTraining { get; set; }
        public DateTime? FinalAcceptance { get; set; }
        public string? InstallationRemarks { get; set; }
        public bool IsActive { get; set; }
        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual GroupName? GroupName { get; set; }
        public virtual EqupmentName? EqupmentName { get; set; }
        public virtual Controlled? Controlled { get; set; }
        public virtual FcLc? FcLc { get; set; }
        public virtual DgdpNssd? DgdpNssd { get; set; }
        public virtual FinancialYear? FinancialYear { get; set; }
        public virtual BudgetCode? BudgetCode { get; set; }
        public virtual ICollection<ProjectBaseSchoolName>? ProjectBaseSchoolNames { get; set; }


    }
}
