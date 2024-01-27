using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.RunningTimes.Requests.Queries
{
    public class GetSelectedRunningTimeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
