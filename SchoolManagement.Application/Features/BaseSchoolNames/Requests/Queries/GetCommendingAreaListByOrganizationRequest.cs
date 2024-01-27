using MediatR;
using SchoolManagement.Application.DTOs.BaseSchoolNames;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries
{
    public class GetCommendingAreaListByOrganizationRequest : IRequest<List<BaseSchoolNameDto>>
    {
        public int FirstLevel { get; set; }
    }
}

 