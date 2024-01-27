using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Modules.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Modules.Handlers.Queries
{
    public class GetSelectedModuleByTypeHandler : IRequestHandler<GetSelectedModuleRequests, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Module> _ModuleRepository;

         
        public GetSelectedModuleByTypeHandler(ISchoolManagementRepository<Module> ModuleRepository)
        {
            _ModuleRepository = ModuleRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedModuleRequests request, CancellationToken cancellationToken)
        {
            ICollection<Module> Modules = await _ModuleRepository.FilterAsync(x =>x.IsActive);
            List<SelectedModel> selectModels = Modules.Select(x => new SelectedModel
            {
                Text = x.Title,
                Value = x.ModuleId
            }).ToList();
            return selectModels;
        }
    }
}
