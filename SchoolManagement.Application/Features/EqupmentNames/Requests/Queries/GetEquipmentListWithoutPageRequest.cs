using MediatR;
using SchoolManagement.Application.DTOs.EqupmentNames;

namespace SchoolManagement.Application.Features.EqupmentNames.Requests.Queries 
{
    public class GetEquipmentListWithoutPageRequest : IRequest<List<EqupmentNameDto>>
    {
        public string? SearchText { get; set; }
    }
}
