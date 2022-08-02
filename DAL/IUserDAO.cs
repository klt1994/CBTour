using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public interface IUserDAO
    {
        List<UserDM> ReadUsers(SqlParameter[] parameter, string statement);

        List<UserDM> GetUsers();

        void CreateUser(UserDM item);

        void UpdateUser(UserDM user);

        void DeleteUser(string email);

        UserDM GetUser(string email);
    }
}
