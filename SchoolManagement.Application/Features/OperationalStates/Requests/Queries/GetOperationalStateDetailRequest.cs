using MediatR;
using SchoolManagement.Application.DTOs.OperationalStates;
using SchoolManagement.Application.DTOs.ShipInformationTypes;

namespace SchoolManagement.Application.Features.OperationalStates.Requests.Queries
{
    public class GetOperationalStateDetailRequest : IRequest<OperationalStateDto>
    {
        public int OperationalStateId { get; set; }
    }
}
