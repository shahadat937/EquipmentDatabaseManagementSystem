using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentCategorys;
using SchoolManagement.Application.Features.EquipmentCategorys.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Handlers.Queries
{
    public class GetEquipmentCategoryDetailRequestHandler : IRequestHandler<GetEquipmentCategoryDetailRequest, EquipmentCategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<EquipmentCategory> _EquipmentCategoryRepository;
        public GetEquipmentCategoryDetailRequestHandler(ISchoolManagementRepository<EquipmentCategory> EquipmentCategoryRepository, IMapper mapper)
        {
            _EquipmentCategoryRepository = EquipmentCategoryRepository;
            _mapper = mapper;
        }
        public async Task<EquipmentCategoryDto> Handle(GetEquipmentCategoryDetailRequest request, CancellationToken cancellationToken)
        {
            var EquipmentCategory = await _EquipmentCategoryRepository.Get(request.EquipmentCategoryId);
            return _mapper.Map<EquipmentCategoryDto>(EquipmentCategory);
        }
    }
}
