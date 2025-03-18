namespace SchoolManagement.Application.DTOs.Project
{
    public class ProjectDto : IProjectDto
    {
        public int ProjectId { get; set; }
        public int? EqupmentNameId { get; set; }
        public string? EqupmentName { get; set; }
        public double? Qty { get; set; }
        public int? DgdpNssdId { get; set; }
        public string? DgdpNssdName { get; set; }
        public decimal? EPrice { get; set; }
        public int? FcLcId { get; set; }
        public string? FcLcName { get; set; }
        public int? GroupNameId { get; set; }
        public string? GroupName { get; set; }
        public int? BudgetCodeId { get; set; }
        public string? BudgetCodeName { get; set; }
        public string? BudgetCode { get; set; }
        public int? FinancialYearId { get; set; }
        public string? FinancialYearName { get; set; }
        public int? ControlledId { get; set; }
        public string? ControlledName { get; set; }
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
        public List<int?> BaseSchoolNameId { get; set; }
        

        public List<ProjectBaseSchoolNameDto>?  BaseSchoolNameDtos { get; set; }

    }
}
