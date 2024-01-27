using MediatR;
using SchoolManagement.Application.DTOs.OperationalStatuss;

namespace SchoolManagement.Application.Features.OperationalStatuss.Requests.Queries
{
    public class GetOperationalStatusDetailRequest : IRequest<OperationalStatusDto>
    {
        public int OperationalStatusId { get; set; }
    }
}
