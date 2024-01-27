using MediatR;
using SchoolManagement.Application.DTOs.CodeValueType;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.CodeValueTypes.Requests.Commands 
{
    public class CreateCodeValueTypeCommand : IRequest<BaseCommandResponse> 
    {
        public CreateCodeValueTypeDto CodeValueTypeDto { get; set; }      

    }
}
