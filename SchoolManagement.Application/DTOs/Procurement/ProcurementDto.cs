namespace SchoolManagement.Application.DTOs.Procurement
{
    public class ProcurementDto : IProcurementDto
    {
        public int ProcurementId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? ProcurementMethodId { get; set; }
        public int? EnvelopeId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? ProcurementTypeId { get; set; }
        public int? GroupNameId { get; set; }
        public int? EqupmentNameId { get; set; }
        public int? ControlledId { get; set; }
        public int? FcLcId { get; set; }
        public int? DgdpNssdId { get; set; }
        public int? TecId { get; set; }
        public int? TenderOpeningDateTypeId { get; set; }
        public double? Qty { get; set; }
        public double? EPrice { get; set; }
        public DateTime? SentToDgdpNssdDate { get; set; }
        public DateTime? TenderOpeningDate { get; set; }
        public DateTime? OfferReceivedDate { get; set; }
        public DateTime? ContractMinSentDate { get; set; }
        public DateTime? ContractMinReceived { get; set; }
        public DateTime? SentForContractDate { get; set; }
        public DateTime? ContractSignedDate { get; set; }
        public string? AIP { get; set; }
        public DateTime? ClarificationToOemSentDate { get; set; }
        public DateTime? ClarificationToOemReceivedDate { get; set; }
        public DateTime? ClarificationToUserSentDate { get; set; }
        public DateTime? ClarificationToUserReceivedDate { get; set; }
        public DateTime? TechTecSentDate { get; set; }
        public DateTime? TechTecReceivedDate { get; set; }
        public DateTime? MinForFoSentDate { get; set; }
        public DateTime? MinForFoReceivedDate { get; set; }
        public DateTime? SentToDtsDate { get; set; }
        public DateTime? FoReceivedDate { get; set; }
        public DateTime? FoTecSentDate { get; set; }
        public DateTime? FoTecReceivedDate { get; set; }
        public DateTime? FinalContractMinSentDate { get; set; }
        public DateTime? FinalContractMinReceivedDate { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? SchoolName { get; set; }
        public string? ProcurementMethodName { get; set; }
        public string? EnvelopeName { get; set; }
        public string? ProcurementTypeName { get; set; }
        public string? GroupName { get; set; }
        public string? EqupmentName { get; set; }
        public string? ControlledName { get; set; }
        public string? FcLcName { get; set; }
        public string? DgdpNssdName { get; set; }
        public string? TenderOpeningDateTypeName { get; set; }
        public string? TecName { get; set; }
        public string? PaymentStatusName { get; set; }

    }
}
