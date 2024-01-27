using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.AcquisitionMethod;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Handlers.Queries
{
    public class GetAcquisitionMethodDetailRequestHandler : IRequestHandler<GetAcquisitionMethodDetailRequest, AcquisitionMethodDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<AcquisitionMethod> _AcquisitionMethodRepository;
        public GetAcquisitionMethodDetailRequestHandler(ISchoolManagementRepository<AcquisitionMethod> AcquisitionMethodRepository, IMapper mapper)
        {
            _AcquisitionMethodRepository = AcquisitionMethodRepository;
            _mapper = mapper;
        }
        public async Task<AcquisitionMethodDto> Handle(GetAcquisitionMethodDetailRequest request, CancellationToken cancellationToken)
        {
            var AcquisitionMethod = await _AcquisitionMethodRepository.Get(request.AcquisitionMethodId);
            return _mapper.Map<AcquisitionMethodDto>(AcquisitionMethod);
        }
    }
}
