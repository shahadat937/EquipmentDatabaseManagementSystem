using MediatR;
using SchoolManagement.Application.DTOs.EqupmentNames;

namespace SchoolManagement.Application.Features.EqupmentNames.Requests.Queries
{
    public class GetEqupmentNameDetailRequest : IRequest<EqupmentNameDto>
    {
        public int EqupmentNameId { get; set; }
    }
}
