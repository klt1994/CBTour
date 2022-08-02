using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class UserDAO : IUserDAO
    {
        public string ConnectionString = @"Server=.\SQLEXPRESS;Database=CBTour;Trusted_Connection=True;";
        private IDAO DataWriter;

        public UserDAO(IDAO dataWriter)
        {
            DataWriter = dataWriter;
        }
        public List<UserDM> ReadUsers(SqlParameter[] parameter, string statement)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameter != null)
                    {
                        command.Parameters.AddRange(parameter);
                    }
                    connection.Open();
                    SqlDataReader data = command.ExecuteReader();
                    List<UserDM> users = new List<UserDM>();
                    while (data.Read())
                    {
                        UserDM user = new UserDM();
                        user.Email = data["Email"].ToString();
                        user.Password = data["Password"].ToString();
                        user.ID = (Int32)data["ID"];
                        user.Role = data["Role"].ToString();
                        users.Add(user);
                    }
                    return users;
                }
            }
        }
        public List<UserDM> GetUsers()
        {
            return ReadUsers(null, "GetUsers");
        }
        public UserDM GetUser(string email)
        {
            return ReadUsers(new SqlParameter[] { new SqlParameter("@Email", email) }, "GetUserDetail")[0];
        }
        public void CreateUser(UserDM user)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@Email", user.Email)
                ,new SqlParameter("@Password", user.Password)
                ,new SqlParameter("@Role", user.Role)
            };
            DAO dataWriter = new DAO();
            dataWriter.Write(parameter, "RegisterUser");
        }
        public void UpdateUser(UserDM user)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@Email", user.Email)
                ,new SqlParameter("@Password", user.Password)
                ,new SqlParameter("@ID", user.ID)
                ,new SqlParameter("@Role", user.Role)
            };
            DAO dataWriter = new DAO();
            dataWriter.Write(parameter, "UpdateUser");
        }
        public void DeleteUser(string email)
        {
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@Email", email)
            };
            DAO dataWriter = new DAO();
            dataWriter.Write(parameter, "DeleteUser");
        }
    }
}
