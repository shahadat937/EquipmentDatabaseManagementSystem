using AutoMapper;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using SchoolManagement.Application.DTOs.EqupmentNames;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Queries;

namespace SchoolManagement.Application.Features.EqupmentNames.Handlers.Queries
{
    public class GeEquipmentListWithoutPageRequestHandler : IRequestHandler<GetEquipmentListWithoutPageRequest, List<EqupmentNameDto>>
    {
          
        private readonly ISchoolManagementRepository<EqupmentName> _EqupmentNameRepository;

        private readonly IMapper _mapper;
         
        public GeEquipmentListWithoutPageRequestHandler(ISchoolManagementRepository<EqupmentName> EqupmentNameRepository, IMapper mapper)
        {
            _EqupmentNameRepository = EqupmentNameRepository; 
            _mapper = mapper;
        }
         
        public async Task<List<EqupmentNameDto>> Handle(GetEquipmentListWithoutPageRequest request, CancellationToken cancellationToken)
        {
            var EqupmentNames = _EqupmentNameRepository.FilterWithInclude(x=>x.IsActive, "EquipmentCategory").OrderBy(x => x.EquipmentCategory.MenuPosition);

            var EqupmentNameDtos = _mapper.Map<List<EqupmentNameDto>>(EqupmentNames);

            return EqupmentNameDtos; 
        }
    }
}
