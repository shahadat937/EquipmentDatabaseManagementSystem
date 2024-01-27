using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Tec;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Tecs.Requests.Queries
{
    public class GetTecListRequest : IRequest<PagedResult<TecDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
