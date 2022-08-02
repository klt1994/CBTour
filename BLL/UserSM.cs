using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL
{
    public class UserSM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int ID { get; set; }
        public string Role { get; set; }

        public UserSM() { }

        public UserSM(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
