using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bank
{
    class Program
    {

        static void Main(string[] args)
        {
            LoginMenu loginMenu = new LoginMenu();
            loginMenu.loginMenu(null);

        }
    }
}