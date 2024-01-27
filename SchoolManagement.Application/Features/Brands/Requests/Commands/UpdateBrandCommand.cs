using MediatR;
using SchoolManagement.Application.DTOs.Brands;

namespace SchoolManagement.Application.Features.Brands.Requests.Commands
{
    public class UpdateBrandCommand : IRequest<Unit>
    { 
        public BrandDto BrandDto { get; set; }
    }
}
 