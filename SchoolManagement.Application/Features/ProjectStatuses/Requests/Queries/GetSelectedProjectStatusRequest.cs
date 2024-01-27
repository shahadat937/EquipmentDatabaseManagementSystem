using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProjectStatuses.Requests.Queries
{
    public class GetSelectedProjectStatusRequest : IRequest<List<SelectedModel>>
    {
    }
} 
