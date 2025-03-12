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
        public double? EPrice { get; set; }
        public int? FcLcId { get; set; }
        public int? GroupNameId { get; set; }
        public string? BudgetCode { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ControlledId { get; set; }
        public DateTime? ContractSigningDate { get; set; }
        public string? ForeignTrainingScheduleAndContractReceived { get; set; }
        public string? ForeignTrainingApprovalForOfficerNomination { get; set; }
        public string? ForeignTrainingGoApproval { get; set; }
        public DateTime? ForeignTraningDate { get; set; }
        public string? ForeignTrainingRemark { get; set; }
        public string? FatSchedule { get; set; }
        public string? FatAcceptance { get; set; }
        public string? FatApprovalForOfficerNomination { get; set; }
        public string? FatGoApproval { get; set; }
        public DateTime? FatDate { get; set; }
        public string? FatRemarks { get; set; }
        public string? ShipmentAndDelivery { get; set; }
        public string? STW { get; set; }
        public string? HAT { get; set; }
        public string? SAT { get; set; }
        public string? LocalTraining { get; set; }
        public string? FinalAcceptance { get; set; }
        public string? InstallationRemarks { get; set; }
        public bool IsActive { get; set; }
        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual GroupName? GroupName { get; set; }
        public virtual EqupmentName? EqupmentName { get; set; }
        public virtual Controlled? Controlled { get; set; }
        public virtual FcLc? FcLc { get; set; }
        public virtual DgdpNssd? DgdpNssd { get; set; }
        public virtual FinancialYear? FinancialYear { get; set; }

        public virtual ICollection<ProjectBaseSchoolName>? ProjectBaseSchoolNames { get; set; }


    }
}
