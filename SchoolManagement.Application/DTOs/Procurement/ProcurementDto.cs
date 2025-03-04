namespace SchoolManagement.Application.DTOs.Procurement
{
    public class ProcurementDto : IProcurementDto
    {
        public int ProcurementId { get; set; }
        public int? EqupmentNameId { get; set; }
        public List<int?> BaseSchoolNameId { get; set; }
        public double? Qty { get; set; }
        public int? DgdpNssdId { get; set; }
        public double? EPrice { get; set; }
        public int? FcLcId { get; set; }
        public int? GroupNameId { get; set; }
        public string? BudgetCode { get; set; }
        public int? FinancialYearId { get; set; }
        public string? FinancialYearName { get; set; }
        public int? ControlledId { get; set; }
        public DateTime? SentForAIPDate { get; set; }
        public DateTime? AIPApprovalDate { get; set; }
        public DateTime? IndentSentDate { get; set; }
        public int? NumberOfTenderOpening { get; set; }
        public DateTime? TenderOpeningDate { get; set; }
        public DateTime? TenderFloatedDate { get; set; }
        public DateTime? OfferReceivedDateAndUpdateEvaluation { get; set; }
        public DateTime? SentForContractDate { get; set; }
        public DateTime? ContractSignedDate { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public string? SchoolName { get; set; }
        public string? GroupName { get; set; }
        public string? EqupmentName { get; set; }
        public string? ControlledName { get; set; }
        public string? FcLcName { get; set; }
        public string? DgdpNssdName { get; set; }

        public List<ProcurementBaseSchoolNameDto>?  BaseSchoolNameDtos { get; set; }
        public List<ProcurementTenderOpeningDto>? ProcurementTenderOpeningDto { get; set; }

    }
}
