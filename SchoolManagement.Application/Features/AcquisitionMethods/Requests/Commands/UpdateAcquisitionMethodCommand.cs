using MediatR;
using SchoolManagement.Application.DTOs.AcquisitionMethod;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Requests.Commands
{
    public class UpdateAcquisitionMethodCommand : IRequest<Unit>
    { 
        public AcquisitionMethodDto AcquisitionMethodDto { get; set; }
    }
}
 