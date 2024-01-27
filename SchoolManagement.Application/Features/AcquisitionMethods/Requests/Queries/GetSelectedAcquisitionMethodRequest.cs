using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries
{
    public class GetSelectedAcquisitionMethodRequest : IRequest<List<SelectedModel>>
    {
    }
} 
