using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    class User
    {
        string username;
        string password;

        public User(string username,string password)
        {
            this.username = username;
            this.password = password;
        }

        public string getUsername()
        {
            return this.username;
        }

        public string getPassword()
        {
            return this.password;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public bool vaildUser(string username, string password)
        {
            return this.username.Equals(username) && this.password.Equals(password);
        }

        public void printUser()
        {
            Console.WriteLine("{0}{1}", username, password);
        }
    }
}
