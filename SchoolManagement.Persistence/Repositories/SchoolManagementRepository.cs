using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SchoolManagement.Persistence.Repositories
{
    public class SchoolManagementRepository<T> : GenericRepository<SchoolManagementDbContext, T>, ISchoolManagementRepository<T>
          where T : class
    {
        internal SqlConnection Connection;
        public SchoolManagementRepository(SchoolManagementDbContext context) : base(context)
        {
            Connection = new SqlConnection(context.Database.GetConnectionString());
        }

        public DataTable ExecWithStoreProcedure(string query, IDictionary<string, object> values)
        {

            using (Connection)

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                foreach (KeyValuePair<string, object> item in values)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                DataTable table = new DataTable();
                using (var reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                    return table;
                }
            }
        }

        public DataTable ExecWithSqlQuery(string query)
        {
            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(query, Connection);
                DataTable dt = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                return dt;
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                if
                    (Connection.State == ConnectionState.Open)
                {

                    Connection.Close();
                }
            }



        }

        public int ExecNoneQuery(string query)
        {
            try
            {
                Connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, Connection))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                if
                    (Connection.State == ConnectionState.Open)
                {

                    Connection.Close();
                }
            }


        }
    }
}
