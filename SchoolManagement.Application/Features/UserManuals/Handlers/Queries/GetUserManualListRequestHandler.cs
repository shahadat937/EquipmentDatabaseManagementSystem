using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.UserManual;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.UserManuals.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
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
        private readonly ISchoolManagementRepository<AspNetRoles> _roleRepository; // Add this dependency for roles
        private readonly IMapper _mapper;

        public GetUserManualListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.UserManual> UserManualRepository,
                                                ISchoolManagementRepository<AspNetRoles> roleRepository,
                                                IMapper mapper)
        {
            _UserManualRepository = UserManualRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserManualDto>> Handle(GetUserManualListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            // Get the roles collection from the repository
            var rolesCollection = await _roleRepository.GetAll();
            var roles = rolesCollection
                         .Where(r => string.IsNullOrEmpty(request.QueryParams.SearchText) ||
                                     r.Name.Contains(request.QueryParams.SearchText)) // Filter by RoleName
                         .ToDictionary(r => r.Id, r => r.Name); // Map role ID to role name

            IQueryable<SchoolManagement.Domain.UserManual> UserManuals = _UserManualRepository.FilterWithInclude(x =>
                (x.RoleName.Contains(request.QueryParams.SearchText) || string.IsNullOrEmpty(request.QueryParams.SearchText)));

            var totalCount = UserManuals.Count();
            UserManuals = UserManuals.OrderByDescending(x => x.UserManualId)
                                     .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                                     .Take(request.QueryParams.PageSize);

            var UserManualDtos = _mapper.Map<List<UserManualDto>>(UserManuals);

            // Replace the RoleName (which is an ID) with the actual Role Name from the roles dictionary
            foreach (var manual in UserManualDtos)
            {
                if (roles.ContainsKey(manual.RoleName)) // Check if the RoleName exists in roles dictionary
                {
                    manual.RoleName = roles[manual.RoleName]; // Replace Role ID with Role Name
                }
            }

            var result = new PagedResult<UserManualDto>(UserManualDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }

}
