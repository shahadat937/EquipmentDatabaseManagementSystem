using MediatR;
using SchoolManagement.Application.DTOs.FcLc;

namespace SchoolManagement.Application.Features.FcLcs.Requests.Queries
{
    public class GetFcLcDetailRequest : IRequest<FcLcDto>
    {
        public int FcLcId { get; set; }
    }
}
