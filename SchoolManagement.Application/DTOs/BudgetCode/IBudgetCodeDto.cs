namespace SchoolManagement.Application.DTOs.BudgetCode
{
    public interface IBudgetCodeDto
    {
            public int BudgetCodeId { get; set; }
            public string BudgetCodeName { get; set; } 
            public string Code { get; set; } 
            public string? Remarks { get; set; }
            public bool IsActive { get; set; }
        }

    }

