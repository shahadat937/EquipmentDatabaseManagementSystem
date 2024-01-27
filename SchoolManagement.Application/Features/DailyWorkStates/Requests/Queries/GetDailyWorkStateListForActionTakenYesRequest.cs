using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.DailyWorkState;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries
{
    public class GetDailyWorkStateListForActionTakenYesRequest : IRequest<List<DailyWorkStateDto>>
    {
            
    }

    //public class GetDailyWorkStateListForActionTakenNoRequest : IRequest<PagedResult<DailyWorkStateDto>>
    //{
    //    public QueryParams QueryParams { get; set; }
    //}
}
