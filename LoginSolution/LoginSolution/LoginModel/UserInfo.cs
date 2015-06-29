using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Model
{
    public class UserInfo
    {
        private int userId;
        private string userName;
        private string password;
        private string email;

        public int UserId
        {
            get{ return  userId ;}
            set{ userId = value; }
        }
        public string  UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
      
    }
}
