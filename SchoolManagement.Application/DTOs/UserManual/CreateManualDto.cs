using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.UserManual
{
    public class CreateManualDto
    {
        public IFormFile Doc { get; set; }
        public CreateUserManualDto UserManualForm { get; set; }
    }
}
