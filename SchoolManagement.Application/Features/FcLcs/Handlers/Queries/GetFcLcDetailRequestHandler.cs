using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FcLc;
using SchoolManagement.Application.Features.FcLcs.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.FcLcs.Handlers.Queries
{
    public class GetFcLcDetailRequestHandler : IRequestHandler<GetFcLcDetailRequest, FcLcDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<FcLc> _FcLcRepository;
        public GetFcLcDetailRequestHandler(ISchoolManagementRepository<FcLc> FcLcRepository, IMapper mapper)
        {
            _FcLcRepository = FcLcRepository;
            _mapper = mapper;
        }
        public async Task<FcLcDto> Handle(GetFcLcDetailRequest request, CancellationToken cancellationToken)
        {
            var FcLc = await _FcLcRepository.Get(request.FcLcId);
            return _mapper.Map<FcLcDto>(FcLc);
        }
    }
}
