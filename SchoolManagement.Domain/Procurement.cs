﻿using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class Procurement : BaseDomainEntity
    {

        public Procurement()
        {
            ProcurementBaseSchoolNames = new HashSet<ProcurementBaseSchoolName>();
            ProcurementTenderOpenings = new HashSet<ProcurementTenderOpening>();
        }
        public int ProcurementId { get; set; }
        public int? EqupmentNameId { get; set; }
        //public int? BaseSchoolNameId { get; set; }
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
        public DateTime? TenderFloatedDate { get; set; }
        public DateTime? OfferReceivedDateAndUpdateEvaluation { get; set; }
        public DateTime? SentForContractDate { get; set; }
        public DateTime? ContractSignedDate { get; set; }       
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual GroupName? GroupName { get; set; }
        public virtual EqupmentName? EqupmentName { get; set; }
        public virtual Controlled? Controlled { get; set; }
        public virtual FcLc? FcLc { get; set; }
        public virtual DgdpNssd? DgdpNssd { get; set; }
        public virtual FinancialYear FinancialYear { get; set; }
 
        //public virtual TenderOpeningDateType? TenderOpeningDateType { get; set; }

        public virtual ICollection<ProcurementBaseSchoolName>? ProcurementBaseSchoolNames { get; set; }
        public virtual ICollection<ProcurementTenderOpening>? ProcurementTenderOpenings { get; set; }
    }
}
