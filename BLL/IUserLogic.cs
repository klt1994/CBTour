using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public interface IUserLogic
    {
        UserSM VerifyRole(string email, string password);
        List<UserSM> GetUserList();
        void RegisterUser(string email, string password, string role);
        void UpdateUserInfo(int id, string email, string password, string role);
        void DeleteUser(string email);
        UserSM GetUserDetail(string email);
    }
}
