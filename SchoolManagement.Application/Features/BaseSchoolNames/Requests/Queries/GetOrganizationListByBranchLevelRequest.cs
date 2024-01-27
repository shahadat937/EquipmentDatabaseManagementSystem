using MediatR;
using SchoolManagement.Application.DTOs.BaseSchoolNames;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries
{
    public class GetOrganizationListByBranchLevelRequest : IRequest<List<BaseSchoolNameDto>>
    {
        
    }
}

  