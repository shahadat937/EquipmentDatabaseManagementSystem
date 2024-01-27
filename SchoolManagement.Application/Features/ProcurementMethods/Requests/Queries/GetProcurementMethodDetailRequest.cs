using MediatR;
using SchoolManagement.Application.DTOs.ProcurementMethod;

namespace SchoolManagement.Application.Features.ProcurementMethods.Requests.Queries
{
    public class GetProcurementMethodDetailRequest : IRequest<ProcurementMethodDto>
    {
        public int ProcurementMethodId { get; set; }
    }
}
