using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Features.Users.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Users.Handlers.Queries 
{ 
    public class GetUsersListRequestHandler : IRequestHandler<GetUsersListRequest, PagedResult<UserDto>>
    { 

        private readonly ISchoolManagementRepository<User> _UserRepository;    

        private readonly IMapper _mapper;  
         
        public GetUsersListRequestHandler(ISchoolManagementRepository<User> UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository; 
            _mapper = mapper;  
        }

        public async Task<PagedResult<UserDto>> Handle(GetUsersListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString()); 

            IQueryable<User> Users = _UserRepository.FilterWithInclude(x => (x.UserName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Role", "BranchInfo");
            var totalCount = Users.Count();
            Users = Users.OrderByDescending(x => x.UserId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
              
            var UsersDtos = _mapper.Map<List<UserDto>>(Users); 
            var result = new PagedResult<UserDto>(UsersDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }
}
