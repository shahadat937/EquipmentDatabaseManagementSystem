using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.UserManual;
using SchoolManagement.Application.Features.UserManuals.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.UserManuals.Handlers.Queries
{
    public class GetUserManualDetailRequestHandler : IRequestHandler<GetUserManualDetailRequest, UserManualDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.UserManual> _UserManualRepository;
        public GetUserManualDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.UserManual> UserManualRepository, IMapper mapper)
        {
            _UserManualRepository = UserManualRepository;
            _mapper = mapper;
        }
        public async Task<UserManualDto> Handle(GetUserManualDetailRequest request, CancellationToken cancellationToken)
        {
            var UserManual = await _UserManualRepository.Get(request.UserManualId);
            return _mapper.Map<UserManualDto>(UserManual);
        }
    }
}
