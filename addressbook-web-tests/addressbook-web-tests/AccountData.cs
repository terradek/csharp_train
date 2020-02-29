using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    class AccountData
    {
        string user, password;
        public AccountData(string user, string password)
        {
            this.user = user;
            this.password = password;
        }
        public string User
        {
            get { return user; }
            set { user = value; }
        }
        public string Password 
        {
            get { return password; }
            set { password = value; } 
        }
    }
}
