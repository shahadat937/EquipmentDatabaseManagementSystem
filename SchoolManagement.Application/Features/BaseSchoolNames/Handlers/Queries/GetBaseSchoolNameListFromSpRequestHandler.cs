using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;
using System.Data;
using SchoolManagement.Application.Features.CourseDurations.Requests.Queries;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Queries
{
    public class GetBaseSchoolNameListFromSpRequestHandler : IRequestHandler<GetBaseSchoolNameListFromSpRequest, object>
    {

        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;

        private readonly IMapper _mapper;

        public GetBaseSchoolNameListFromSpRequestHandler(ISchoolManagementRepository<BaseSchoolName> BaseSchoolNameRepository, IMapper mapper)
        {
            _BaseSchoolNameRepository = BaseSchoolNameRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetBaseSchoolNameListFromSpRequest request, CancellationToken cancellationToken)
        {   
            var spQuery = String.Format("exec [spGetBaseNameListAndCount]");
            
            DataTable dataTable = _BaseSchoolNameRepository.ExecWithSqlQuery(spQuery);
            return dataTable;
         
        }
    }
}
