using MediatR;
using SchoolManagement.Application.DTOs.ReturnTypes;

namespace SchoolManagement.Application.Features.ReturnTypes.Requests.Queries
{
    public class GetReturnTypeDetailRequest : IRequest<ReturnTypeDto>
    {
        public int ReturnTypeId { get; set; }
    }
}
