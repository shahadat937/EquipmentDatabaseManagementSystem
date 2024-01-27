using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Tecs.Requests.Queries
{
    public class GetSelectedTecRequest : IRequest<List<SelectedModel>>
    {
    }
} 
