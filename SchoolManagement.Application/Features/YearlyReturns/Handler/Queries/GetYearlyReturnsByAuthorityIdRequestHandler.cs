using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.YearlyReturns.Request.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Handler.Queries
{
    public class GetYearlyReturnsByAuthorityIdRequestHandler : IRequestHandler<GetYearlyReturnsByAuthorityIdRequest, object>
    {
        private readonly ISchoolManagementRepository<YearlyReturn> _yearlyReturn;
        private readonly IMapper _mapper;

        private readonly IConfiguration _config;

        public GetYearlyReturnsByAuthorityIdRequestHandler(ISchoolManagementRepository<YearlyReturn> yearlyReturn, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _yearlyReturn = yearlyReturn;
            _config = config;
        }
        public async Task<object> Handle(GetYearlyReturnsByAuthorityIdRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [spGetYearlyReturnByAuthorityId] {0}, {1}, {2}, {3}",

           string.IsNullOrEmpty(request.QueryParams.SearchText) ? "''" : $"'{request.QueryParams.SearchText}'",
           request.QueryParams.PageSize,
           request.QueryParams.PageNumber,
           request.AuthorityId);

            DataTable dataTable = _yearlyReturn.ExecWithSqlQuery(spQuery);

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["FileUpload"] != DBNull.Value && row["FileUpload"] is string fileUpload && !string.IsNullOrEmpty(fileUpload))
                {
                    row["FileUpload"] = $"{_config["ApiUrl"]}{fileUpload}";
                }
            }

            return dataTable;
        }
    }
}
