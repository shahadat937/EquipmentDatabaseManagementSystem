using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReturnTypes;
using SchoolManagement.Application.Features.ReturnTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ReturnTypes.Handlers.Queries
{
    public class GetReturnTypeDetailRequestHandler : IRequestHandler<GetReturnTypeDetailRequest, ReturnTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ReturnType> _ReturnTypeRepository;
        public GetReturnTypeDetailRequestHandler(ISchoolManagementRepository<ReturnType> ReturnTypeRepository, IMapper mapper)
        {
            _ReturnTypeRepository = ReturnTypeRepository;
            _mapper = mapper;
        }
        public async Task<ReturnTypeDto> Handle(GetReturnTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var ReturnType = await _ReturnTypeRepository.Get(request.ReturnTypeId);
            return _mapper.Map<ReturnTypeDto>(ReturnType);
        }
    }
}
