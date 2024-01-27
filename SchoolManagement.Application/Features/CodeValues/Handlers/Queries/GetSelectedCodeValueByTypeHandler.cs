using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.CodeValues.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.CodeValues.Handlers.Queries
{
    public class GetSelectedCodeValueByTypeHandler : IRequestHandler<GetSelectedCodeValueByType, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<CodeValue> _CodeValueRepository;


        public GetSelectedCodeValueByTypeHandler(ISchoolManagementRepository<CodeValue> CodeValueRepository)
        {
            _CodeValueRepository = CodeValueRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCodeValueByType request, CancellationToken cancellationToken)
        {
            ICollection<CodeValue> codeValues = await _CodeValueRepository.FilterAsync(x =>x.CodeValueType.Type == request.Type && x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.TypeValue,
                Value = x.CodeValueId
            }).ToList();
            return selectModels;
        }
    }
}
