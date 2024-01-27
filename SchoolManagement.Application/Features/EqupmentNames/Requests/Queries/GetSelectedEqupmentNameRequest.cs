using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EqupmentNames.Requests.Queries
{
    public class GetSelectedEqupmentNameRequest : IRequest<List<SelectedModel>>
    {
    }
} 
 