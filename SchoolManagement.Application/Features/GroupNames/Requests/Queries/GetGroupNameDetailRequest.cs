using MediatR;
using SchoolManagement.Application.DTOs.GroupNames;

namespace SchoolManagement.Application.Features.GroupNames.Requests.Queries
{
    public class GetGroupNameDetailRequest : IRequest<GroupNameDto>
    {
        public int GroupNameId { get; set; }
    }
}
