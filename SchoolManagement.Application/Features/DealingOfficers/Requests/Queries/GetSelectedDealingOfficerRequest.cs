using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DealingOfficers.Requests.Queries
{
    public class GetSelectedDealingOfficerRequest : IRequest<List<SelectedModel>>
    {
    }
} 
