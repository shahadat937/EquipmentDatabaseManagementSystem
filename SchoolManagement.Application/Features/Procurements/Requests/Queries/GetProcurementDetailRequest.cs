using MediatR;
using SchoolManagement.Application.DTOs.Procurement;

namespace SchoolManagement.Application.Features.Procurements.Requests.Queries
{
    public class GetProcurementDetailRequest : IRequest<ProcurementDto>
    {
        public int ProcurementId { get; set; }
    }
}
