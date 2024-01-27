using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ProcurementMethods.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProcurementMethods.Handlers.Queries
{
    public class GetSelectedProcurementMethodRequestHandler : IRequestHandler<GetSelectedProcurementMethodRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ProcurementMethod> _ProcurementMethodRepository;


        public GetSelectedProcurementMethodRequestHandler(ISchoolManagementRepository<ProcurementMethod> ProcurementMethodRepository)
        {
            _ProcurementMethodRepository = ProcurementMethodRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedProcurementMethodRequest request, CancellationToken cancellationToken)
        {
            ICollection<ProcurementMethod> codeValues = await _ProcurementMethodRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ProcurementMethodId
            }).ToList();
            return selectModels;
        }
    }
}
