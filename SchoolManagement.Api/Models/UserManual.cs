namespace SchoolManagement.Api.Models
{
    public class UserManual
    {
        public int UserManualId { get; set; }
        public string RoleName { get; set; }
        public string Doc { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
