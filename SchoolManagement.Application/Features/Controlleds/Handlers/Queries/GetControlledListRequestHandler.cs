using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Controlled;
using SchoolManagement.Application.Features.Controlleds.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Controlleds.Handlers.Queries
{
    public class GetControlledListRequestHandler : IRequestHandler<GetControlledListRequest, PagedResult<ControlledDto>>
    {

        private readonly ISchoolManagementRepository<Controlled> _ControlledRepository;

        private readonly IMapper _mapper;

        public GetControlledListRequestHandler(ISchoolManagementRepository<Controlled> ControlledRepository, IMapper mapper)
        {
            _ControlledRepository = ControlledRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ControlledDto>> Handle(GetControlledListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Controlled> Controlleds = _ControlledRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Controlleds.Count();
            Controlleds = Controlleds.OrderByDescending(x => x.ControlledId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ControlledDtos = _mapper.Map<List<ControlledDto>>(Controlleds);
            var result = new PagedResult<ControlledDto>(ControlledDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
