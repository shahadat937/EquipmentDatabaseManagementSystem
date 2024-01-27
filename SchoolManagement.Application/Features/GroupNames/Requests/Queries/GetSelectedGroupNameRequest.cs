using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.GroupNames.Requests.Queries
{
    public class GetSelectedGroupNameRequest : IRequest<List<SelectedModel>>
    {
    }
} 
