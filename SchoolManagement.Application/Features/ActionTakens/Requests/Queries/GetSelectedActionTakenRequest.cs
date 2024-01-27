using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ActionTakens.Requests.Queries
{
    public class GetSelectedActionTakenRequest : IRequest<List<SelectedModel>>
    {
    }
} 
