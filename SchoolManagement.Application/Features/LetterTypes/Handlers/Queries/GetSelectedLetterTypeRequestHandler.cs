using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.LetterTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.LetterTypes.Handlers.Queries
{
    public class GetSelectedLetterTypeRequestHandler : IRequestHandler<GetSelectedLetterTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<LetterType> _LetterTypeRepository;


        public GetSelectedLetterTypeRequestHandler(ISchoolManagementRepository<LetterType> LetterTypeRepository)
        {
            _LetterTypeRepository = LetterTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedLetterTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<LetterType> codeValues = await _LetterTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.LetterTypeId
            }).ToList();
            return selectModels;
        }
    }
}
