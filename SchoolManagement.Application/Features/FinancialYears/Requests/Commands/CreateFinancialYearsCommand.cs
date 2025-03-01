using MediatR;
using SchoolManagement.Application.DTOs.FinancialYears;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Commands
{
    public class CreateFinancialYearsCommand : IRequest<BaseCommandResponse>
    {
        public CreateFinancialYearsDto FinancialYearsDto { get; set; }
    }
}
