namespace SchoolManagement.Application.DTOs.HalfYearlyRunningTime
{
    public interface IHalfYearlyRunningTimeDto
    {
        public int HalfYearlyRunningTimeId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
