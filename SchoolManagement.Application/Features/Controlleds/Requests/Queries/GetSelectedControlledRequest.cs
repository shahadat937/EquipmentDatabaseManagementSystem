using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Controlleds.Requests.Queries
{
    public class GetSelectedControlledRequest : IRequest<List<SelectedModel>>
    {
    }
} 
