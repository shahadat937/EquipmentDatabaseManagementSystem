using MediatR;
using SchoolManagement.Application.DTOs.ProcurementType;

namespace SchoolManagement.Application.Features.ProcurementTypes.Requests.Queries
{
    public class GetProcurementTypeDetailRequest : IRequest<ProcurementTypeDto>
    {
        public int ProcurementTypeId { get; set; }
    }
}
