using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CodeValueType
{
    public class CodeValueTypeDto : ICodeValueTypeDto
    {
        public int CodeValueTypeId { get; set; }
        public string Type { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}

