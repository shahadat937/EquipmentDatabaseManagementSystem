using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProcurementMethods.Requests.Queries
{
    public class GetSelectedProcurementMethodRequest : IRequest<List<SelectedModel>>
    {
    }
} 
