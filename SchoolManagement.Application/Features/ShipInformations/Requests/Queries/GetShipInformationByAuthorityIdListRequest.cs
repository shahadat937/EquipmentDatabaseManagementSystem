using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Queries
{
    public class GetShipInformationByAuthorityIdListRequest : IRequest<PagedResult<ShipInformationDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int AuthorityId { get; set; }
    }
}
