using MediatR;
using SchoolManagement.Application.DTOs.AcquisitionMethod;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Requests.Commands
{
    public class CreateAcquisitionMethodCommand : IRequest<BaseCommandResponse>
    {
        public CreateAcquisitionMethodDto AcquisitionMethodDto { get; set; }
    }
}
