using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DAO : IDAO
    {
        public string ConnectionString = @"Server=.\SQLEXPRESS;Database=CBTour;Trusted_Connection=True;";

        public int Write(SqlParameter[] parameter, string statement)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameter);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
