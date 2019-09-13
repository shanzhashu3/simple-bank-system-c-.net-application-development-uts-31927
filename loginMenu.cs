using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank
{
    class LoginMenu
    {
        public MainMenu mainmenu = null;
        public User logedInUser = null;

        public LoginMenu()
        {
            mainmenu = new MainMenu(this);
        }

        public void loginMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("|  WELCOME TO SIMPLE BANKING SYSTEM  |");
            Console.WriteLine("|════════════════════════════════════|");
            Console.WriteLine("|            LOGIN TO START          |");
            Console.WriteLine("|                                    |");
            Console.WriteLine("|        User Name:                  |");
            Console.WriteLine("|        Password:                   |");
            Console.WriteLine("╚════════════════════════════════════╝");
            string user_name;
            string password = null;
            if (username == null)
            {
                Console.SetCursorPosition(20, 5);
                user_name = Console.ReadLine();
                password = readPassword();
            }
            else
            {
                user_name = username;
                Console.SetCursorPosition(20, 5);
                Console.WriteLine("{0}", user_name);
                password = readPassword();
            }

            List<User> users = getUsers();
            foreach (User user in users)
            {
                if (user.getUsername().Equals(user_name))
                {
                    if (user.vaildUser(user_name, password))
                    {
                        //Console.WriteLine("                                  ");
                        Console.WriteLine("\n\nValid Credentials!... Please enter");
                        Console.ReadKey();
                        logedInUser = user;
                        mainmenu.showMainMenu();
                    }
                    else
                    {
                        Console.WriteLine("\n\nInvalid!!...Please re-enter");
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            loginMenu(user_name);
                        }
                    }
                }
            }

        }
        private string readPassword()
        {
            string password = null;
            Console.SetCursorPosition(20, 6);
            //string password = Console.ReadLine();
            while (true)
            {
                ConsoleKeyInfo ck = Console.ReadKey(true);
                if (ck.Key != ConsoleKey.Enter)
                {
                    if (ck.Key != ConsoleKey.Backspace)
                    {
                        password += ck.KeyChar.ToString();
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    Console.WriteLine();
                    break;
                }
            }
            return password;
        }
        List<User> getUsers()
        {
            List<User> users = new List<User>();
            string path = @"C:\Users\shan\Desktop\bank\login.txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(path);
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split('|');
                    User user = new User(values[0], values[1]);
                    users.Add(user);

                }
                reader.Close();
            }
            return users;
        }

    }
}