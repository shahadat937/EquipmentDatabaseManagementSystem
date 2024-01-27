using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CodeValues 
{
    public interface ICodeValueDto 
    {
        public int CodeValueId { get; set; }
        public int CodeValueTypeId { get; set; }
        public string Code { get; set; } 
        public string? TypeValue { get; set; }
        public string? AdditonalValue { get; set; }
        public string? DisplayCode { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 