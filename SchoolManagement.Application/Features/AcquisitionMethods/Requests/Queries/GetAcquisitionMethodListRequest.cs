using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.AcquisitionMethod;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries
{
    public class GetAcquisitionMethodListRequest : IRequest<PagedResult<AcquisitionMethodDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
