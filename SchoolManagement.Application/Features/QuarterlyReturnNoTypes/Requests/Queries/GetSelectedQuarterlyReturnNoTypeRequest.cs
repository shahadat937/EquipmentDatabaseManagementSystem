using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Queries
{
    public class GetSelectedQuarterlyReturnNoTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
