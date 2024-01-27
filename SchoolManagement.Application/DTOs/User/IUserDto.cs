using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.User
{
    public interface IUserDto
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public int? BranchInfoId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? LoweredUserName { get; set; }
        public string? MobileAlias { get; set; }
        public bool? IsAnonymous { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public bool? WinPassword { get; set; }
        public string? UserFullName { get; set; }
        public decimal? TransLimit { get; set; }
        public bool ApprovedUser { get; set; }
        public decimal? VerifyLimit { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? SecurityQustion { get; set; }
        public string? Answer { get; set; }
        public string? BankCode { get; set; }
        public int? AttemptCount { get; set; }
        public string? ParmitedBy { get; set; }
        public DateTime? UserExpiryDate { get; set; }
        public int? PasswordValidity { get; set; }
        public DateTime? PasswordChangeDate { get; set; }
        public bool? IsFirstTime { get; set; }
        public long? IsLogin { get; set; }
        public string? Ipaddress { get; set; }
        public string? HostName { get; set; }
        public bool IsActive { get; set; }
    }
}
 