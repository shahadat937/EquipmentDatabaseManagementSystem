using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Brands;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Brands.Requests.Queries
{
    public class GetBrandListRequest : IRequest<PagedResult<BrandDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
