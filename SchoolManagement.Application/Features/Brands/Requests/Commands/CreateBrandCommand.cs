using MediatR;
using SchoolManagement.Application.DTOs.Brands;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Brands.Requests.Commands
{
    public class CreateBrandCommand : IRequest<BaseCommandResponse>
    {
        public CreateBrandDto BrandDto { get; set; }
    }
}
