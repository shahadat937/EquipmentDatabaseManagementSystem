using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class BaseName
    {
        public int BaseNameId { get; set; }
        public int AdminAuthorityId { get; set; }
        public int DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? ForceTypeId { get; set; }
        public string BaseNames { get; set; }
        public string ShortName { get; set; }
        public string BaseLogo { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
