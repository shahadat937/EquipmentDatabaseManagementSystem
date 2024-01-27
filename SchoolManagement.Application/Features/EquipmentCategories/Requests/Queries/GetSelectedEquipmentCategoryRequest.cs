using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Requests.Queries
{
    public class GetSelectedEquipmentCategoryRequest : IRequest<List<SelectedModel>>
    {
    }
} 
