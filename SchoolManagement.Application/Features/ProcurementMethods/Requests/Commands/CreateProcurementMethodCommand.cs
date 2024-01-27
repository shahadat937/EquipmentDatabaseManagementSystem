using MediatR;
using SchoolManagement.Application.DTOs.ProcurementMethod;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ProcurementMethods.Requests.Commands
{
    public class CreateProcurementMethodCommand : IRequest<BaseCommandResponse>
    {
        public CreateProcurementMethodDto ProcurementMethodDto { get; set; }
    }
}
