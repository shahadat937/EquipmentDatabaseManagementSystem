using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DgdpNssds.Requests.Queries
{
    public class GetSelectedDgdpNssdRequest : IRequest<List<SelectedModel>>
    {
    }
} 
