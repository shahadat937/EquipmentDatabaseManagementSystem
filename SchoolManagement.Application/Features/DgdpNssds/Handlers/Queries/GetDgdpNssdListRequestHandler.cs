using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.DgdpNssd;
using SchoolManagement.Application.Features.DgdpNssds.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DgdpNssds.Handlers.Queries
{
    public class GetDgdpNssdListRequestHandler : IRequestHandler<GetDgdpNssdListRequest, PagedResult<DgdpNssdDto>>
    {

        private readonly ISchoolManagementRepository<DgdpNssd> _DgdpNssdRepository;

        private readonly IMapper _mapper;

        public GetDgdpNssdListRequestHandler(ISchoolManagementRepository<DgdpNssd> DgdpNssdRepository, IMapper mapper)
        {
            _DgdpNssdRepository = DgdpNssdRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<DgdpNssdDto>> Handle(GetDgdpNssdListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<DgdpNssd> DgdpNssds = _DgdpNssdRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = DgdpNssds.Count();
            DgdpNssds = DgdpNssds.OrderByDescending(x => x.DgdpNssdId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var DgdpNssdDtos = _mapper.Map<List<DgdpNssdDto>>(DgdpNssds);
            var result = new PagedResult<DgdpNssdDto>(DgdpNssdDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
