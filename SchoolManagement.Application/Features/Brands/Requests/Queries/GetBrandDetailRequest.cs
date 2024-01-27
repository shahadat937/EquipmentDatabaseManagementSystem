using MediatR;
using SchoolManagement.Application.DTOs.Brands;

namespace SchoolManagement.Application.Features.Brands.Requests.Queries
{
    public class GetBrandDetailRequest : IRequest<BrandDto>
    {
        public int BrandId { get; set; }
    }
}
