using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FcLcs.Requests.Queries
{
    public class GetSelectedFcLcRequest : IRequest<List<SelectedModel>>
    {
    }
} 
