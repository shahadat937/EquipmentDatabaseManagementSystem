using AutoMapper;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Queries   
{
    public class GetCommendingAreaListByOrganizationRequestHandler : IRequestHandler<GetCommendingAreaListByOrganizationRequest, List<BaseSchoolNameDto>>
    {

        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;

        private readonly IMapper _mapper;

        public GetCommendingAreaListByOrganizationRequestHandler(ISchoolManagementRepository<BaseSchoolName> BaseSchoolNameRepository, IMapper mapper)
        {
            _BaseSchoolNameRepository = BaseSchoolNameRepository;
            _mapper = mapper;
        }

        public async Task<List<BaseSchoolNameDto>> Handle(GetCommendingAreaListByOrganizationRequest request, CancellationToken cancellationToken)
        {
            IQueryable<BaseSchoolName> BaseSchoolNames = _BaseSchoolNameRepository.FilterWithInclude(x => x.BranchLevel == 2 && x.FirstLevel == request.FirstLevel).OrderBy(x => x.BaseSchoolNameId);
            //var BaseSchoolNames = _BaseSchoolNameRepository.Where(x => x.BaseSchoolNameId == request.BaseSchoolNameId && x.CourseNameId == request.CourseNameId && x.CourseModuleId == request.CourseModuleId && x.Status == request.Status).OrderByDescending(x => x.BaseSchoolNameId);

            var BaseSchoolNameDtos = _mapper.Map<List<BaseSchoolNameDto>>(BaseSchoolNames);

            return BaseSchoolNameDtos;
        }
    }
}
