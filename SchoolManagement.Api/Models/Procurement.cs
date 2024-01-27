using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class Procurement
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
        public double? Eprice { get; set; }
        public DateTime? SentToDgdpNssdDate { get; set; }
        public DateTime? TenderOpeningDate { get; set; }
        public DateTime? OfferReceivedDate { get; set; }
        public DateTime? ContractMinSentDate { get; set; }
        public DateTime? ContractMinReceived { get; set; }
        public DateTime? SentForContractDate { get; set; }
        public DateTime? ContractSignedDate { get; set; }
        public string Aip { get; set; }
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
        public string Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName BaseSchoolName { get; set; }
        public virtual Controlled Controlled { get; set; }
        public virtual DgdpNssd DgdpNssd { get; set; }
        public virtual Envelope Envelope { get; set; }
        public virtual EqupmentName EqupmentName { get; set; }
        public virtual FcLc FcLc { get; set; }
        public virtual GroupName GroupName { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
        public virtual ProcurementMethod ProcurementMethod { get; set; }
        public virtual ProcurementType ProcurementType { get; set; }
        public virtual Tec Tec { get; set; }
        public virtual TenderOpeningDateType TenderOpeningDateType { get; set; }
    }
}
