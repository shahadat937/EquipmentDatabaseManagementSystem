using MediatR;
using SchoolManagement.Application.DTOs.Tec;

namespace SchoolManagement.Application.Features.Tecs.Requests.Queries
{
    public class GetTecDetailRequest : IRequest<TecDto>
    {
        public int TecId { get; set; }
    }
}
