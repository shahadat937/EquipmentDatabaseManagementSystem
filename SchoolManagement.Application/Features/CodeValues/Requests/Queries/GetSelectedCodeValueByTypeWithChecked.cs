using MediatR;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.CodeValues.Requests.Queries
{
    public class GetSelectedCodeValueByTypeWithChecked : IRequest<List<SelectedModelChecked>>
    {
        public string Type { get; set; } 
    }
}
