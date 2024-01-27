using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class DailyWorkState
    {
        public int DailyWorkStateId { get; set; }
        public int? LetterTypeId { get; set; }
        public int? DealingOfficerId { get; set; }
        public int? ActionTakenId { get; set; }
        public int? PriorityId { get; set; }
        public DateTime? Date { get; set; }
        public string WorkFrom { get; set; }
        public string LetterNo { get; set; }
        public string Subject { get; set; }
        public string DealingStaff { get; set; }
        public DateTime? DeadLine { get; set; }
        public string FileUpload { get; set; }
        public string Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ActionTaken ActionTaken { get; set; }
        public virtual DealingOfficer DealingOfficer { get; set; }
        public virtual LetterType LetterType { get; set; }
        public virtual Priority Priority { get; set; }
    }
}
