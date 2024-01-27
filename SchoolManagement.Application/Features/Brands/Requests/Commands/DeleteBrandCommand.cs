using MediatR;

namespace SchoolManagement.Application.Features.Brands.Requests.Commands
{
    public class DeleteBrandCommand : IRequest
    {
        public int BrandId { get; set; }
    }
} 
