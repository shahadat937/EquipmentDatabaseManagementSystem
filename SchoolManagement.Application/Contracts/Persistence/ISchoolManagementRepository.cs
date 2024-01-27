using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SchoolManagement.Application.Contracts.Persistence
{
    public interface ISchoolManagementRepository<T> : IGenericRepository<T> where T : class
    {
        DataTable ExecWithStoreProcedure(string query, IDictionary<string, object> values);
        DataTable ExecWithSqlQuery(string query);
        int ExecNoneQuery(string query);
    }
}
