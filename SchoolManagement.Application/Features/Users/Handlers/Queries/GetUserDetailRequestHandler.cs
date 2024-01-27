using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Features.Users.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Users.Handlers.Queries
{  
    public class GetUsersDetailRequestHandler : IRequestHandler<GetUsersDetailRequest, UserDto>
    {
        private readonly IMapper _mapper;   
        private readonly ISchoolManagementRepository<User> _UserRepository;       
        public GetUsersDetailRequestHandler(ISchoolManagementRepository<User> UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;    
            _mapper = mapper; 
        } 
        public async Task<UserDto> Handle(GetUsersDetailRequest request, CancellationToken cancellationToken)
        {
            var User = await _UserRepository.Get(request.Id);    
            return _mapper.Map<UserDto>(User);    
        }
    }
}
