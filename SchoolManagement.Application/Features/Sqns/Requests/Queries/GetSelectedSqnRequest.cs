using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Sqns.Requests.Queries
{
    public class GetSelectedSqnRequest : IRequest<List<SelectedModel>>
    {
    }
} 
