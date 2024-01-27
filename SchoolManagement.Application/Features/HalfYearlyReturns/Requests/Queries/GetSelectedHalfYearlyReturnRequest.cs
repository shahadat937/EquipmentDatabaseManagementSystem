using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries
{
    public class GetSelectedHalfYearlyReturnRequest : IRequest<List<SelectedModel>>
    {
    }
} 
