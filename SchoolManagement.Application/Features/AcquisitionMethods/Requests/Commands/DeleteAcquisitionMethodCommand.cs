using MediatR;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Requests.Commands
{
    public class DeleteAcquisitionMethodCommand : IRequest
    {
        public int AcquisitionMethodId { get; set; }
    }
} 
