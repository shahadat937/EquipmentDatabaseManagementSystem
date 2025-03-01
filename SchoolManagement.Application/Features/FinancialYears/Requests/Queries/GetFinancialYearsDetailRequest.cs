using MediatR;
using SchoolManagement.Application.DTOs.FinancialYears;

namespace SchoolManagement.Application.Features.FinancialYears.Requests.Queries
{
    public class GetFinancialYearsDetailRequest : IRequest<FinancialYearsDto>
    {
        public int FinancialYearId { get; set; }
    }
}
