using SchoolManagement.Doamin;
using SchoolManagement.Domain;
using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class MonthlyReturn:BaseDomainEntity
    {
        public int MonthlyReturnId { get; set; }
        public int? AuthorityId { get; set; }
        public int? BaseNameId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? EquipmentCategoryId { get; set; }
        public int? EqupmentNameId { get; set; }
        public int? ReportingMonthId { get; set; }
        public int? ReturnTypeId { get; set; }
        public int? OperationalStatusId { get; set; }
        public string? DamageDescription { get; set; }
        public string? PresentCondition { get; set; }
        public DateTime? ReportingDate { get; set; }
        public DateTime? TimeOfDefect { get; set; }
        public string? UploadDocument { get; set; }
        public string? Remarks { get; set; }
       
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public int? ShipEquipmentInfoId { get; set; }
        public int? ReturnQty { get; set; }

        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        public virtual EquipmentCategory? EquipmentCategory { get; set; }
        public virtual EqupmentName? EqupmentName { get; set; }
        public virtual OperationalStatus? OperationalStatus { get; set; }
        public virtual ReportingMonth? ReportingMonth { get; set; }
        public virtual ReturnType? ReturnType { get; set; }
        //public virtual ShipEquipmentInfo? ShipEquipmentInfo { get; set; }
    }
}
