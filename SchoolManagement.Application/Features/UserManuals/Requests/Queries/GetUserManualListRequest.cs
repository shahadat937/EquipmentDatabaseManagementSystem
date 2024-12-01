using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.UserManual;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.UserManuals.Requests.Queries
{
    public class GetUserManualListRequest : IRequest<PagedResult<UserManualDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
