using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BookTypes.Requests.Queries
{
    public class GetSelectedBookTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
