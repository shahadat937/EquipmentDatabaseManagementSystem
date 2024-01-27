using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes;
using SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Handlers.Queries
{
    public class GetTenderOpeningDateTypeDetailRequestHandler : IRequestHandler<GetTenderOpeningDateTypeDetailRequest, TenderOpeningDateTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<TenderOpeningDateType> _TenderOpeningDateTypeRepository;
        public GetTenderOpeningDateTypeDetailRequestHandler(ISchoolManagementRepository<TenderOpeningDateType> TenderOpeningDateTypeRepository, IMapper mapper)
        {
            _TenderOpeningDateTypeRepository = TenderOpeningDateTypeRepository;
            _mapper = mapper;
        }
        public async Task<TenderOpeningDateTypeDto> Handle(GetTenderOpeningDateTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var TenderOpeningDateType = await _TenderOpeningDateTypeRepository.Get(request.TenderOpeningDateTypeId);
            return _mapper.Map<TenderOpeningDateTypeDto>(TenderOpeningDateType);
        }
    }
}
