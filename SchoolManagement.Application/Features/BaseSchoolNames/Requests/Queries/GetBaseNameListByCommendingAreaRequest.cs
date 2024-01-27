using MediatR;
using SchoolManagement.Application.DTOs.BaseSchoolNames;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries
{
    public class GetBaseNameListByCommendingAreaRequest : IRequest<List<BaseSchoolNameDto>>
    {
        public int SecondLevel { get; set; }
    }
}

 