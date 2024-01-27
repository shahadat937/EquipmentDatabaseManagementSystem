using MediatR;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.CodeValueTypes.Requests.Queries
{
    public class GetSelectedCodeValueTypeRequest : IRequest<List<SelectedModel>>
    {
    }
}
