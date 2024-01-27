using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Users.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Users.Handlers.Queries
{
    public class GetSelectedUserByRoleHandler : IRequestHandler<GetSelectedUserByRole, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<User> _UserRepository;


        public GetSelectedUserByRoleHandler(ISchoolManagementRepository<User> UserRepository)
        {
            _UserRepository = UserRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedUserByRole request, CancellationToken cancellationToken)
        {
            ICollection<User> Users = await _UserRepository.FilterAsync(x =>x.IsActive);
            List<SelectedModel> selectModels = Users.Select(x => new SelectedModel
            {
                Text = x.UserName,
                Value = x.UserId
            }).ToList();
            return selectModels;
        }
    }
}
