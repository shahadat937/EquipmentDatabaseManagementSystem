using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Controlleds.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Controlleds.Handlers.Queries
{
    public class GetSelectedControlledRequestHandler : IRequestHandler<GetSelectedControlledRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Controlled> _ControlledRepository;


        public GetSelectedControlledRequestHandler(ISchoolManagementRepository<Controlled> ControlledRepository)
        {
            _ControlledRepository = ControlledRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedControlledRequest request, CancellationToken cancellationToken)
        {
            ICollection<Controlled> codeValues = await _ControlledRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ControlledId
            }).ToList();
            return selectModels;
        }
    }
}
