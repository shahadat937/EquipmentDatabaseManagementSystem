namespace SchoolManagement.Application.DTOs.Procurement
{
    public class CreateProcurementDto : IProcurementDto
    {
        public int ProcurementId { get; set; }
        public int? EqupmentNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public double? Qty { get; set; }
        public int? DgdpNssdId { get; set; }
        public double? EPrice { get; set; }
        public int? FcLcId { get; set; }
        public int? GroupNameId { get; set; }
        public string? BudgetCode { get; set; }
        public int? FinancialYearId { get; set; }
        public int? ControlledId { get; set; }
        public DateTime? SentForAIPDate { get; set; }
        public DateTime? AIPApprovalDate { get; set; }
        public DateTime? IndentSentDate { get; set; }
        public int? NumberOfTenderOpening { get; set; }
        public DateTime? TenderOpeningDate { get; set; }
        public DateTime? OfferReceivedDateAndUpdateEvaluation { get; set; }
        public DateTime? SentForContractDate { get; set; }
        public DateTime? ContractSignedDate { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
        public List<ProcurementBaseSchoolNameDto>? BaseSchoolNamesDtos { get; set; }
        public List<ProcurementTenderOpeningDto>? ProcurementTenderOpeningDto { get; set; }
    }
}
 