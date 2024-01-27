using MediatR;
using SchoolManagement.Application.DTOs.AcquisitionMethod;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries
{
    public class GetAcquisitionMethodDetailRequest : IRequest<AcquisitionMethodDto>
    {
        public int AcquisitionMethodId { get; set; }
    }
}
