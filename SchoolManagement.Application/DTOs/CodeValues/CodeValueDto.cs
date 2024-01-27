using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CodeValues
{
    public class CodeValueDto : ICodeValueDto
    {

        public int CodeValueId { get; set; }
        public int CodeValueTypeId { get; set; }
        public string Code { get; set; } = null!;
        public string? TypeValue { get; set; }
        public string? AdditonalValue { get; set; }
        public string? DisplayCode { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }


        public string? CodeValueType { get; set; }
    }
}

