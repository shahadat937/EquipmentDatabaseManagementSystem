using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FinancialYears.Requests.Queries
{
    public class GetSelectedFinancialYearsRequest : IRequest<List<SelectedModel>>
    {
    }
} 
