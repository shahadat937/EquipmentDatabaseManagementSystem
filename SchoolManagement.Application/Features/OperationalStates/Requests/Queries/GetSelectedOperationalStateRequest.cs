using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.OperationalStates.Requests.Queries
{
    public class GetSelectedOperationalStateRequest : IRequest<List<SelectedModel>>
    {
    }
} 
