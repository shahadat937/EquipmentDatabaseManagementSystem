using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.UserManual;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.UserManuals.Requests.Queries;
using SchoolManagement.Application.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.UserManuals.Handlers.Queries
{
    public class GetUserManualListRequestHandler : IRequestHandler<GetUserManualListRequest, PagedResult<UserManualDto>>
    {
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.UserManual> _UserManualRepository;

        private readonly IMapper _mapper;

        public GetUserManualListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.UserManual> UserManualRepository, IMapper mapper)
        {
            _UserManualRepository = UserManualRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserManualDto>> Handle(GetUserManualListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.UserManual> UserManuals = _UserManualRepository.FilterWithInclude(x => (x.RoleName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = UserManuals.Count();
            UserManuals = UserManuals.OrderByDescending(x => x.UserManualId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var UserManualDtos = _mapper.Map<List<UserManualDto>>(UserManuals);
            var result = new PagedResult<UserManualDto>(UserManualDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
