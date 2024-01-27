using MediatR;
using SchoolManagement.Application.DTOs.CodeValueType;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.CodeValueTypes.Requests.Commands
{
    public class UpdateCodeValueTypeCommand : IRequest<Unit>  
    { 
        public CodeValueTypeDto CodeValueTypeDto { get; set; }     
    }
}
