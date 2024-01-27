using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.LetterTypes.Requests.Queries
{
    public class GetSelectedLetterTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
