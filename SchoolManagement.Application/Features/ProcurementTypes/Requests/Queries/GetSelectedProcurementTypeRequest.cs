using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProcurementTypes.Requests.Queries
{
    public class GetSelectedProcurementTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
