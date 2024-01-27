namespace SchoolManagement.Application.DTOs.DailyWorkState
{
    public class DailyWorkStateDto : IDailyWorkStateDto
    {
        public int DailyWorkStateId { get; set; }
        public int? LetterTypeId { get; set; }
        public int? DealingOfficerId { get; set; }
        public int? ActionTakenId { get; set; }
        public int? PriorityId { get; set; }
        public DateTime? Date { get; set; }
        public string? WorkFrom { get; set; }
        public string? LetterNo { get; set; }
        public string? Subject { get; set; }
        public string? DealingStaff { get; set; }
        public DateTime? DeadLine { get; set; }
        public string? FileUpload { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? LetterType { get; set; }
        public string? DealingOfficer { get; set; }
        public string? ActionTaken { get; set; }
        public string? Priority { get; set; }
    }
}
