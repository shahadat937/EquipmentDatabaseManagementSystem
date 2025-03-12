using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BudgetCode.Requests.Queries
{
    public class GetSelectedBudgetCodeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
