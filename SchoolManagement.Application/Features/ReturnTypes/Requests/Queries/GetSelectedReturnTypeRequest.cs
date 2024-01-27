using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReturnTypes.Requests.Queries
{
    public class GetSelectedReturnTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
