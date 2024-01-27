using MediatR;

namespace SchoolManagement.Application.Features.FcLcs.Requests.Commands
{
    public class DeleteFcLcCommand : IRequest
    {
        public int FcLcId { get; set; }
    }
} 
