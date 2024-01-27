using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReturnTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReturnTypes.Handlers.Queries
{
    public class GetSelectedReturnTypeRequestHandler : IRequestHandler<GetSelectedReturnTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ReturnType> _ReturnTypeRepository;


        public GetSelectedReturnTypeRequestHandler(ISchoolManagementRepository<ReturnType> ReturnTypeRepository)
        {
            _ReturnTypeRepository = ReturnTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedReturnTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<ReturnType> codeValues = await _ReturnTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ReturnTypeId
            }).ToList();
            return selectModels;
        }
    }
}
