using MediatR;

namespace SchoolManagement.Application.Features.Tecs.Requests.Commands
{
    public class DeleteTecCommand : IRequest
    {
        public int TecId { get; set; }
    }
} 
