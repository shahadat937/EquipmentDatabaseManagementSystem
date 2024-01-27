using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Procurements.Requests.Queries
{
    public class GetSelectedProcurementRequest : IRequest<List<SelectedModel>>
    {
    }
} 
