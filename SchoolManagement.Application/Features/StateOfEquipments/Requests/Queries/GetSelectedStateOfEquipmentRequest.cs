using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.StateOfEquipments.Requests.Queries
{
    public class GetSelectedStateOfEquipmentRequest : IRequest<List<SelectedModel>>
    {
    }
} 
