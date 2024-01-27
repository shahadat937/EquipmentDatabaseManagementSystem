using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Brands.Requests.Queries
{
    public class GetSelectedBrandRequest : IRequest<List<SelectedModel>>
    {
    }
} 
