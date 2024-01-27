using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.OperationalStates;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.OperationalStates.Requests.Queries
{
    public class GetOperationalStateListRequest : IRequest<PagedResult<OperationalStateDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
