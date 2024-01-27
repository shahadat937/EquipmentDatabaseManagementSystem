using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes;
using SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Handlers.Queries
{
    public class GetQuarterlyReturnNoTypeDetailRequestHandler : IRequestHandler<GetQuarterlyReturnNoTypeDetailRequest, QuarterlyReturnNoTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<QuarterlyReturnNoType> _QuarterlyReturnNoTypeRepository;
        public GetQuarterlyReturnNoTypeDetailRequestHandler(ISchoolManagementRepository<QuarterlyReturnNoType> QuarterlyReturnNoTypeRepository, IMapper mapper)
        {
            _QuarterlyReturnNoTypeRepository = QuarterlyReturnNoTypeRepository;
            _mapper = mapper;
        }
        public async Task<QuarterlyReturnNoTypeDto> Handle(GetQuarterlyReturnNoTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var QuarterlyReturnNoType = await _QuarterlyReturnNoTypeRepository.Get(request.QuarterlyReturnNoTypeId);
            return _mapper.Map<QuarterlyReturnNoTypeDto>(QuarterlyReturnNoType);
        }
    }
}
