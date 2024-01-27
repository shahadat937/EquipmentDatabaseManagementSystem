using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Handlers.Queries
{
    public class GetSelectedAcquisitionMethodRequestHandler : IRequestHandler<GetSelectedAcquisitionMethodRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<AcquisitionMethod> _AcquisitionMethodRepository;


        public GetSelectedAcquisitionMethodRequestHandler(ISchoolManagementRepository<AcquisitionMethod> AcquisitionMethodRepository)
        {
            _AcquisitionMethodRepository = AcquisitionMethodRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedAcquisitionMethodRequest request, CancellationToken cancellationToken)
        {
            ICollection<AcquisitionMethod> codeValues = await _AcquisitionMethodRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.AcquisitionMethodId
            }).ToList();
            return selectModels;
        }
    }
}
