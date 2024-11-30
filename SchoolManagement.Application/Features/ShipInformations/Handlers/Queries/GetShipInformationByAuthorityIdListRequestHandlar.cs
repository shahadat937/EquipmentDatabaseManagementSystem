using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Queries
{
    public class GetShipInformationByAuthorityIdListRequestHandlar : IRequestHandler<GetShipInformationByAuthorityIdListRequest, PagedResult<ShipInformationDto>>
    {
        private readonly ISchoolManagementRepository<ShipInformation> _ShipInformationRepository;

        private readonly IMapper _mapper;

        public GetShipInformationByAuthorityIdListRequestHandlar(ISchoolManagementRepository<ShipInformation> ShipInformationRepository, IMapper mapper)
        {
            _ShipInformationRepository = ShipInformationRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<ShipInformationDto>> Handle(GetShipInformationByAuthorityIdListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ShipInformation> ShipInformations = _ShipInformationRepository.FilterWithInclude(x => x.AuthorityId == request.AuthorityId && String.IsNullOrEmpty(request.QueryParams.SearchText) || (x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText)) || (x.Authority.Contains(request.QueryParams.SearchText)), "BaseSchoolName", "AuthorityNavigation", "BaseName", "Sqn", "OperationalStatus");
            var totalCount = ShipInformations.Count();
            ShipInformations = ShipInformations.OrderByDescending(x => x.ShipInformationId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ShipInformationDtos = _mapper.Map<List<ShipInformationDto>>(ShipInformations);
            var result = new PagedResult<ShipInformationDto>(ShipInformationDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }
}
