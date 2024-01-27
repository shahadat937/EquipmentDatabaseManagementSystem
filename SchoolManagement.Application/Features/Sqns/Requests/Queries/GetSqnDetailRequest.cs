using MediatR;
using SchoolManagement.Application.DTOs.Sqns;

namespace SchoolManagement.Application.Features.Sqns.Requests.Queries
{
    public class GetSqnDetailRequest : IRequest<SqnDto>
    {
        public int SqnId { get; set; }
    }
}
