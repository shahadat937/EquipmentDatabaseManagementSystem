using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EquipmentTypes.Requests.Queries
{
    public class GetSelectedEquipmentTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
