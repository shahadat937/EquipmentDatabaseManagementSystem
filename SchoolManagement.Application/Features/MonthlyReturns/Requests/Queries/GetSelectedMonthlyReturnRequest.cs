using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries
{
    public class GetSelectedMonthlyReturnRequest : IRequest<List<SelectedModel>>
    {
    }
} 
