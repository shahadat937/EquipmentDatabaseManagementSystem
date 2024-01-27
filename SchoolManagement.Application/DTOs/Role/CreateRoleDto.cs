﻿using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Role
{
    public class CreateRoleDto : IRoleDto
    {
       
        public string RoleName { get; set; } = null!;
        
    }
}
