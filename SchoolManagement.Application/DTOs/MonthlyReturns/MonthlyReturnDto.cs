using SchoolManagement.Doamin;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.DTOs.MonthlyReturns
{
    public class MonthlyReturnDto : IMonthlyReturnDto
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
        public string? EquipmentCategory { get; set; }
        public string? EqupmentName { get; set; }
        public string? OperationalStatus { get; set; }
        public string? ReportingMonth { get; set; }
        public string? ReturnType { get; set; }
    }
}
