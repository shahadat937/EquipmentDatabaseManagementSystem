using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Queries
{  
    public class GetBaseSchoolNameDetailRequestHandler : IRequestHandler<GetBaseSchoolNameDetailRequest, BaseSchoolNameDto>
    {
        private readonly IMapper _mapper;   
        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;       
        public GetBaseSchoolNameDetailRequestHandler(ISchoolManagementRepository<BaseSchoolName> BaseSchoolNameRepository, IMapper mapper)
        {
            _BaseSchoolNameRepository = BaseSchoolNameRepository;    
            _mapper = mapper; 
        } 
        public async Task<BaseSchoolNameDto> Handle(GetBaseSchoolNameDetailRequest request, CancellationToken cancellationToken)
        {
            var BaseSchoolName = await _BaseSchoolNameRepository.Get(request.Id);    
            return _mapper.Map<BaseSchoolNameDto>(BaseSchoolName);    
        }
    }
}
