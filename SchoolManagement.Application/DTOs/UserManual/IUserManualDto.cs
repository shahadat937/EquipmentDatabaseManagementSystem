using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.UserManual
{
    public interface IUserManualDto
    {
        public int UserManualId { get; set; }
        public string? RoleName { get; set; }
        public string? Doc { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
