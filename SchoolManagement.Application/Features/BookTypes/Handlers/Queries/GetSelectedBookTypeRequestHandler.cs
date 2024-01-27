using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.BookTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BookTypes.Handlers.Queries
{
    public class GetSelectedBookTypeRequestHandler : IRequestHandler<GetSelectedBookTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<BookType> _BookTypeRepository;


        public GetSelectedBookTypeRequestHandler(ISchoolManagementRepository<BookType> BookTypeRepository)
        {
            _BookTypeRepository = BookTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBookTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<BookType> codeValues = await _BookTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.BookTypeId
            }).ToList();
            return selectModels;
        }
    }
}
