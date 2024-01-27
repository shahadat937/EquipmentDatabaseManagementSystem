namespace SchoolManagement.Application.DTOs.BookUserManualBRInfo
{
    public class BookUserManualBRInfoDto : IBookUserManualBRInfoDto
    {
        public int BookUserManualBRInfoId { get; set; }
        public int? BookTypeId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public string? BookName { get; set; }
        public string? UploadDocument { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? BookType { get; set; }
        public string? ShipName { get; set; }

    }
}
