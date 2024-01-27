using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.OperationalStatuss.Requests.Queries
{
    public class GetSelectedOperationalStatusRequest : IRequest<List<SelectedModel>>
    {
    }
} 
