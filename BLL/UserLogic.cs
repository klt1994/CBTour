using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;
using System.IO;

namespace BLL
{
    public class UserLogic : IUserLogic
    {
        private IUserDAO UserData;

        public UserLogic(UserDAO userData)
        {
            UserData = userData;
        }
        public UserSM VerifyRole(string email, string password)
        {
            UserSM temp = new UserSM();
            List<UserSM> users = Mapper.Map<List<UserSM>>(UserData.GetUsers());
            foreach (UserSM user in users)
            {
                if (email == user.Email && password == user.Password)
                {
                    temp = user;
                    return user;
                }
            }
            return temp;
        }
        public List<UserSM> GetUserList()
        {
            List<UserSM> users = Mapper.Map<List<UserSM>>(UserData.GetUsers());
            return users;
        }
        public UserSM GetUserDetail(string email)
        {
            return Mapper.Map<UserSM>(UserData.GetUser(email));
        }

        public void RegisterUser(string email, string password, string role)
        {
            if (role == null)
            {
                role = "User";
            }
            UserSM user = new UserSM();
            user.Email = email;
            user.Password = password;
            user.Role = role;
            UserData.CreateUser(Mapper.Map<UserDM>(user));
        }
        public void UpdateUserInfo(int id, string email, string password, string role)
        {
            UserSM user = new UserSM();
            user.Email = email;
            user.Password = password;
            user.ID = id;
            user.Role = role;
            UserData.UpdateUser(Mapper.Map<UserDM>(user));
        }
        public void DeleteUser(string email)
        {
            UserData.DeleteUser(email);
        }

    }
}
