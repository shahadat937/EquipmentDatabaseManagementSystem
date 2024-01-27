using MediatR;
using SchoolManagement.Application.DTOs.BaseSchoolNames;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries
{
    public class GetBaseSchoolListByBaseNameRequest : IRequest<List<BaseSchoolNameDto>>
    {
        public int ThirdLevel { get; set; }
    }
}

 