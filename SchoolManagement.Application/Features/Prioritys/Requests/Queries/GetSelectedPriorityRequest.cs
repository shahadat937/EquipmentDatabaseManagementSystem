using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Prioritys.Requests.Queries
{
    public class GetSelectedPriorityRequest : IRequest<List<SelectedModel>>
    {
    }
} 
