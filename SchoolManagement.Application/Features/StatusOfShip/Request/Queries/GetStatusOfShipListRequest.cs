using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.StatusOfShip;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfShip.Request.Queries
{
    public class GetStatusOfShipListRequest: IRequest<PagedResult<StatusOfShipDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
