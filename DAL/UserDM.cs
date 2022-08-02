using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int ID { get; set; }
        public string Role { get; set; }

        public UserDM() { }
    }
}
