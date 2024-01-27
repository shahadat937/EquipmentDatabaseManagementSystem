using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.LetterTypes;
using SchoolManagement.Application.Features.LetterTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.LetterTypes.Handlers.Queries
{
    public class GetLetterTypeDetailRequestHandler : IRequestHandler<GetLetterTypeDetailRequest, LetterTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<LetterType> _LetterTypeRepository;
        public GetLetterTypeDetailRequestHandler(ISchoolManagementRepository<LetterType> LetterTypeRepository, IMapper mapper)
        {
            _LetterTypeRepository = LetterTypeRepository;
            _mapper = mapper;
        }
        public async Task<LetterTypeDto> Handle(GetLetterTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var LetterType = await _LetterTypeRepository.Get(request.LetterTypeId);
            return _mapper.Map<LetterTypeDto>(LetterType);
        }
    }
}
