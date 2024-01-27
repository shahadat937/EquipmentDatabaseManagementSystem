using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Tec;
using SchoolManagement.Application.Features.Tecs.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Tecs.Handlers.Queries
{
    public class GetTecListRequestHandler : IRequestHandler<GetTecListRequest, PagedResult<TecDto>>
    {

        private readonly ISchoolManagementRepository<Tec> _TecRepository;

        private readonly IMapper _mapper;

        public GetTecListRequestHandler(ISchoolManagementRepository<Tec> TecRepository, IMapper mapper)
        {
            _TecRepository = TecRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<TecDto>> Handle(GetTecListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Tec> Tecs = _TecRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Tecs.Count();
            Tecs = Tecs.OrderByDescending(x => x.TecId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var TecDtos = _mapper.Map<List<TecDto>>(Tecs);
            var result = new PagedResult<TecDto>(TecDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
