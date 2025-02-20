using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Request.Queries
{
    public class GetQuarterlyReturnListRequest : IRequest<PagedResult<QuarterlyReturnDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int? ShipId { get; set; }
    }
}
