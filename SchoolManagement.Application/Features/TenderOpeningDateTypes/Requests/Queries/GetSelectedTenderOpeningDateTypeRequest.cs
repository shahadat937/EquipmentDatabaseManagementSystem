using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Queries
{
    public class GetSelectedTenderOpeningDateTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
