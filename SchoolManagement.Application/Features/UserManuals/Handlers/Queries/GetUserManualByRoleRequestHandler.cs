using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.UserManuals.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.UserManuals.Handlers.Queries
{
    public class GetUserManualByRoleRequestHandler : IRequestHandler<GetUserManualByRoleRequest, object>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<UserManual> _UserManualRepository;
        public GetUserManualByRoleRequestHandler(ISchoolManagementRepository<UserManual> UserManualRepository, IMapper mapper)
        {
            _UserManualRepository = UserManualRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetUserManualByRoleRequest request, CancellationToken cancellationToken)
        {
            var UserManuals = _UserManualRepository.FilterWithInclude(x => (x.RoleName.Contains(request.RoleName)));
            //var UserManual = await _UserManualRepository.Get(request.UserManualId);
            return UserManuals;
        }
    }
}
