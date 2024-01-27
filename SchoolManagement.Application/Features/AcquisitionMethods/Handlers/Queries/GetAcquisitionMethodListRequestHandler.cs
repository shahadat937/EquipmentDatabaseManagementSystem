using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.AcquisitionMethod;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Handlers.Queries
{
    public class GetAcquisitionMethodListRequestHandler : IRequestHandler<GetAcquisitionMethodListRequest, PagedResult<AcquisitionMethodDto>>
    {

        private readonly ISchoolManagementRepository<AcquisitionMethod> _AcquisitionMethodRepository;

        private readonly IMapper _mapper;

        public GetAcquisitionMethodListRequestHandler(ISchoolManagementRepository<AcquisitionMethod> AcquisitionMethodRepository, IMapper mapper)
        {
            _AcquisitionMethodRepository = AcquisitionMethodRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<AcquisitionMethodDto>> Handle(GetAcquisitionMethodListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<AcquisitionMethod> AcquisitionMethods = _AcquisitionMethodRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = AcquisitionMethods.Count();
            AcquisitionMethods = AcquisitionMethods.OrderByDescending(x => x.AcquisitionMethodId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var AcquisitionMethodDtos = _mapper.Map<List<AcquisitionMethodDto>>(AcquisitionMethods);
            var result = new PagedResult<AcquisitionMethodDto>(AcquisitionMethodDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
