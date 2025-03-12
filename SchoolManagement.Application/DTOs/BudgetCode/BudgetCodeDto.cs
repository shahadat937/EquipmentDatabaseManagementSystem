namespace SchoolManagement.Application.DTOs.BudgetCode
{
    public class BudgetCodeDto : IBudgetCodeDto
    {
        public int BudgetCodeId { get; set; }
        public string BudgetCodeName { get; set; }
        public string Code { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
