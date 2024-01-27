using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Tecs.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Tecs.Handlers.Queries
{
    public class GetSelectedTecRequestHandler : IRequestHandler<GetSelectedTecRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Tec> _TecRepository;


        public GetSelectedTecRequestHandler(ISchoolManagementRepository<Tec> TecRepository)
        {
            _TecRepository = TecRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedTecRequest request, CancellationToken cancellationToken)
        {
            ICollection<Tec> codeValues = await _TecRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.TecId
            }).ToList();
            return selectModels;
        }
    }
}
