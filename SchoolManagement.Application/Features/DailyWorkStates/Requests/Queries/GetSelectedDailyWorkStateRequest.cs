using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries
{
    public class GetSelectedDailyWorkStateRequest : IRequest<List<SelectedModel>>
    {
    }
} 
