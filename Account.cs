using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    class Account
    {
        string firstname;
        string lastname;
        string address;
        string phone;
        string email;
        double balance;

        public Account(double balacne, string firstname, string lastname, string address, string phone, string email)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.balance = 0;
        }

        public string getFirstname()
        {
            return this.firstname;
        }

        public string getLastName()
        {
            return this.lastname;
        }

        public string getAddress()
        {
            return this.address;
        }

        public string getPhone()
        {
            return this.phone;
        }

        public string getEmail()
        {
            return this.email;
        }

        public void setFirstname(string firstname)
        {
            this.firstname = firstname;
        }

        public void setLastname(string lastname)
        {
            this.lastname = lastname;
        }

        public void setAddress(string address)
        {
            this.address = address;
        }

        public void setPhone(string phone)
        {
            this.phone = phone;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }


    }
}
