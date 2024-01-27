using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Queries
{
    public class GetCountSchoolFromSpRequestHandler : IRequestHandler<GetCountSchoolFromSpRequest, object>
    {

        private readonly ISchoolManagementRepository<BaseSchoolName> _baseSchoolNameRepository;

        private readonly IMapper _mapper;

        public GetCountSchoolFromSpRequestHandler(ISchoolManagementRepository<BaseSchoolName> baseSchoolNameRepository, IMapper mapper)
        {
            _baseSchoolNameRepository = baseSchoolNameRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetCountSchoolFromSpRequest request, CancellationToken cancellationToken)
        {
            
            var spQuery = String.Format("exec [spGetSchoolCount] " /*request.CourseTypeId, request.CurrentDate*/);
            
            DataTable dataTable = _baseSchoolNameRepository.ExecWithSqlQuery(spQuery);
            //return dataTable;

            var schoolCount = "";

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                schoolCount = row["column1"].ToString();
            }

            return schoolCount;

        }
    }
}
