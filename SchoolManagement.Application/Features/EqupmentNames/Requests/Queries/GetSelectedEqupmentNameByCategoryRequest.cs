using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EqupmentNames.Requests.Queries
{
    public class GetSelectedEqupmentNameByCategoryRequest : IRequest<List<SelectedModel>>
    {
        public int EqepmentCategoryId { get; set;}
    }
} 
 