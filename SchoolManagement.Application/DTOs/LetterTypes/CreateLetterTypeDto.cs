namespace SchoolManagement.Application.DTOs.LetterTypes
{
    public class CreateLetterTypeDto : ILetterTypeDto
    {
        public int LetterTypeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 