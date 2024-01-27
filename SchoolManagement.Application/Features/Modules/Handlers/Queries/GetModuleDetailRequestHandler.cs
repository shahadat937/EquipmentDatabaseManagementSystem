using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Modules;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;
using SchoolManagement.Application.Features.Modules.Requests.Queries;

namespace SchoolManagement.Application.Features.Modules.Handlers.Queries
{
    public class GetModulesDetailRequestHandler : IRequestHandler<GetModuleDetailRequest, ModuleDto>
    {
        private readonly IMapper _mapper;   
        private readonly ISchoolManagementRepository<Module> _ModuleRepository;       
        public GetModulesDetailRequestHandler(ISchoolManagementRepository<Module> ModuleRepository, IMapper mapper)
        {
            _ModuleRepository = ModuleRepository;    
            _mapper = mapper; 
        } 
        public async Task<ModuleDto> Handle(GetModuleDetailRequest request, CancellationToken cancellationToken)
        {
            var Module = await _ModuleRepository.Get(request.Id);    
            return _mapper.Map<ModuleDto>(Module);    
        }
    }
}
