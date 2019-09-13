using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Windows;
using System.IO;

namespace bank
{
    class MainMenu
    {
        public LoginMenu loginMenu;
        public MainMenu(LoginMenu loginMenu)
        {
            this.loginMenu = loginMenu;
        }
        public void showMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("|  WELCOME TO SIMPLE BANKING SYSTEM  |");
            Console.WriteLine("|════════════════════════════════════|");
            Console.WriteLine("|   1. Create a new account          |");
            Console.WriteLine("|   2. Search for an account         |");
            Console.WriteLine("|   3. Deposit                       |");
            Console.WriteLine("|   4. Withdraw                      |");
            Console.WriteLine("|   5. A/C statement                 |");
            Console.WriteLine("|   6. Delete account                |");
            Console.WriteLine("|   7. Exit                          |");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine("|   Enter your choice (1-7):         |");
            Console.WriteLine("╚════════════════════════════════════╝");

            Console.SetCursorPosition(28, 11);
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(" \n\n Please enter number from 1 to 7.");
                Console.ReadKey();
                showMainMenu();
            }

            switch (choice)
            {
                case 1:
                    creatNewAccount(null,null,null,null,null);
                    break;
                case 2:
                    searchAccount();
                    break;
                case 3:
                    deposit();
                    break;
                case 4:
                    withdraw();
                    break;
                case 5:
                    displayAccount(null,0.00,null,null,null,null,null);
                    break;
                case 6:
                    deleteAccount();
                    break;
                case 7:
                    exit();
                    break;
            }
        }
        
        //1. creatAccount
        public void creatNewAccount(string firstname , string lastname, string address, string phone, string email)
        {

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("|        CREATE A NEW ACCOUNT        |");
            Console.WriteLine("|════════════════════════════════════|");
            Console.WriteLine("|          ENTER THE DETAILS         |");
            Console.WriteLine("|                                    |");
            Console.WriteLine("|    First Name:                     |");
            Console.WriteLine("|    Last Name:                      |");
            Console.WriteLine("|    Address:                        |");
            Console.WriteLine("|    Phone:                          |");
            Console.WriteLine("|    Email:                          |");
            Console.WriteLine("╚════════════════════════════════════╝");
            //first name
            if (firstname != null)
            {
                Console.SetCursorPosition(17, 5);
                Console.WriteLine(firstname);
            }
            else
            {
                Console.SetCursorPosition(17, 5);
                firstname = Console.ReadLine();
            }

            //lastname
            if (lastname != null)
            {
                Console.SetCursorPosition(16,6);
                Console.WriteLine(lastname);
            }
            else
            {
                Console.SetCursorPosition(16, 6);
                lastname = Console.ReadLine();
            }

            //address
            if (address != null)
            {
                Console.SetCursorPosition(14, 7);
                Console.WriteLine(address);
            }
            else
            {
                Console.SetCursorPosition(14, 7);
                address = Console.ReadLine();
            }

            // phone
            if (phone != null)
            {
                Console.SetCursorPosition(12,8);
                Console.WriteLine(phone);
            }
            else
            {
                Console.SetCursorPosition(12, 8);
                phone = Console.ReadLine();
                int outInt;
                if (phone.Length > 10 || !Int32.TryParse(phone, out outInt))
                {
                    Console.WriteLine("\n\nPhone number should not be more than 10 characters or must be number ");
                    phone = null;
                    creatNewAccount(firstname,lastname,address,phone,email);
                }

            }

            //email
            Console.SetCursorPosition(12, 9);
            email = Console.ReadLine();
            if (email.Contains('@') || (email.Contains("gmail.com") || email.Contains("outlook.com") || email.Contains("uts.edu.au")))
            {
                Console.WriteLine("\n\n Is the information correct (y/n)?");
                Console.SetCursorPosition(2, 13);
                string choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    Console.WriteLine("\n\n Account Created! details will be provided via email.");
                    Console.WriteLine("\n Account number is:");
                    Console.SetCursorPosition(21, 18);
                    string account_number = Console.ReadLine();
                    string path = @"C:\Users\shan\Desktop\bank\" + account_number + ".txt";
                    int outInt1;
                    while (!Int32.TryParse(account_number, out outInt1) || System.IO.File.Exists(path) || account_number.Length < 6 || account_number.Length > 8)
                    {
                        Console.SetCursorPosition(0, 18);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, 18);
                        Console.Write(" Account number is:");
                        account_number = Console.ReadLine();
                        path = @"C:\Users\shan\Desktop\bank\" + account_number + ".txt";
                    }
                    System.IO.StreamWriter streamwriter = new System.IO.StreamWriter(path);
                    streamwriter.WriteLine(firstname);
                    streamwriter.WriteLine(lastname);
                    streamwriter.WriteLine(address);
                    streamwriter.WriteLine(phone);
                    streamwriter.WriteLine(email);
                    streamwriter.WriteLine(0.0);
                    streamwriter.Close();
                    sendMail(firstname, lastname, address, phone, email);
                }
                else { creatNewAccount(null, null, null, null, null); }
            }
            else
            {
                Console.WriteLine("\n\nPlease using @gmail.com, @outlook.com and @uts.edu.au");
                Console.ReadKey();
                creatNewAccount(firstname,lastname,address,phone,email);
            }



            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.Clear();
                showMainMenu();
            }
        }
        // method sendmail
        public void sendMail(string firstname, string lastname, string address, string phone, string email)
        {
            try { 
            MailMessage mailMesseage = new MailMessage();
            MailAddress fromma = new MailAddress("yinshan19971209@gmail.com");
            MailAddress Toma = new MailAddress(email);
            mailMesseage.From = fromma;
            mailMesseage.To.Add(Toma);
            mailMesseage.Subject = "Account Information";
            mailMesseage.IsBodyHtml = false;
            mailMesseage.Body = "FirstName: "+ firstname + "\nLastName: " + lastname + "\nAddress: " + address +"\nPhone: " + phone + "\nemail: "+ email;//邮件内容   
            mailMesseage.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
                mailMesseage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                //mailMesseage.CC.Add(Toma);
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;

                    client.Credentials = new NetworkCredential("yinshan19971209@gmail.com", "201313ys@");
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(mailMesseage);
                }

                MessageBox.Show("Send successful");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show(ex.Message, "Unsuccessful");
            }
        }

        //2 search for an account
        public void searchAccount()
        {
             
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("|            SEARCH AN ACCOUNT           |");
            Console.WriteLine("|════════════════════════════════════════|");
            Console.WriteLine("|            ENTER THE DETAILS           |");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|    Account Number:                     |");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.SetCursorPosition(21,5);
            string account_number = Console.ReadLine();
            
            int outInt3;
            if (!Int32.TryParse(account_number, out outInt3) || account_number.Length > 10)
            {
                Console.WriteLine("\n\nThe account number must be integer or not more than 10 characters");
                Console.ReadKey();
                searchAccount();
            }
            //if file no exit
            string path = @"C:\Users\shan\Desktop\bank\" + account_number + ".txt";
            while (!System.IO.File.Exists(path))
            {
                Console.WriteLine("\n\nAccount not found!");
                Console.WriteLine("Check another account (y/n)? ");
                Console.SetCursorPosition(29,9);
                char choice1 = Convert.ToChar(Console.ReadLine());
                if (choice1 == 'y')
                {
                    searchAccount();
                }
                if (choice1 == 'n')
                {
                    showMainMenu();
                }

            }
            //if file exit
            Console.WriteLine("\n\nAccount found!");
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("|             Account DETAILS            |");
            Console.WriteLine("|════════════════════════════════════════|");
            Console.WriteLine("|    Account No:                         |");
            Console.WriteLine("|    Account Balance:                    |");
            Console.WriteLine("|    First Name:                         |");
            Console.WriteLine("|    Last Name:                          |");
            Console.WriteLine("|    Address:                            |");
            Console.WriteLine("|    Phone:                              |");
            Console.WriteLine("|    Email:                              |");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.SetCursorPosition(17,12);
            Console.WriteLine(account_number);
            //get the file contents
            System.IO.StreamReader reader = new System.IO.StreamReader(path);
               
            for(int i= 0; i<6; i++)
            {
                int left=0;
                int top=0;
                if (i == 0) { left = 17;
                               top = 14; }
                if (i == 1) { left = 16;top = 15; }
                if (i == 2) { left = 14; top = 16; }
                if (i == 3) { left = 12; top = 17; }
                if (i == 4) { left = 12; top = 18; }
                if (i == 5) { left = 21; top = 13; }
                Console.SetCursorPosition(left, top);
                string line = reader.ReadLine();
                Console.WriteLine(line);

            }
            reader.Close();

            Console.Write("\n\n\n\n\n\nCheck another account (y/n)?");
            char choice2 = Convert.ToChar(Console.ReadLine());
            if (choice2 == 'y')
            {
                searchAccount();
            }
            else
            {
                //go back to main menu
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    showMainMenu();
                }
            }

        }

        //3 Deposit
        public void deposit()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("|                DEPOSIT                 |");
            Console.WriteLine("|════════════════════════════════════════|");
            Console.WriteLine("|             ENTER THE DETAILS          |");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|    Account Number:                     |");
            Console.WriteLine("|    Amount: $                           |");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.SetCursorPosition(21,5);
            string account_number = Console.ReadLine();
            
            int outInt3;
            if (!Int32.TryParse(account_number, out outInt3) || account_number.Length > 10)
            {
                Console.WriteLine("\n\nThe account number must be integer or not more than 10 characters");
                Console.ReadKey();
                deposit();

            }
            string path = @"C:\Users\shan\Desktop\bank\" + account_number + ".txt";
            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine("\n\n Account not found! ");
                Console.WriteLine(" Retry (y/n)?");
                Console.SetCursorPosition(13, 9);
                char choice2 = Convert.ToChar(Console.ReadLine());
                if (choice2 == 'y')
                {
                    deposit();
                }
                if (choice2 == 'n')
                {
                    showMainMenu();
                }
            }


            Console.WriteLine("\n\n Account found! Enter the amount...");
            Console.SetCursorPosition(14,6);
            //StreamReader sr = new StreamReader(path);
            string[] infor = File.ReadAllLines(path);
            String balance = infor[infor.Length-1];
            infor[infor.Length - 1] = (Convert.ToDouble(Console.ReadLine()) + Double.Parse(balance)).ToString();
            StreamWriter sw = new StreamWriter(path);
            foreach(string detail in infor)
            {
                sw.WriteLine(detail);
            }
            sw.Close();

            Console.WriteLine("\n\n Deposit successfull!");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                showMainMenu();
            }


        }

        //4 Withdraw
        public void withdraw()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("|                WITHDRAW                |");
            Console.WriteLine("|════════════════════════════════════════|");
            Console.WriteLine("|             ENTER THE DETAILS          |");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|    Account Number:                     |");
            Console.WriteLine("|    Amount: $                           |");
            Console.WriteLine("╚════════════════════════════════════════╝");


            Console.SetCursorPosition(21, 5);
            string account_number = Console.ReadLine();

            int outInt3;
            if (!Int32.TryParse(account_number, out outInt3) || account_number.Length > 10)
            {
                Console.WriteLine("\n\nThe account number must be integer or not more than 10 characters");
                Console.ReadKey();
                deposit();

            }
            string path = @"C:\Users\shan\Desktop\bank\" + account_number + ".txt";
            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine("\n\n Account not found! ");
                Console.WriteLine(" Retry (y/n)?");
                Console.SetCursorPosition(13, 9);
                char choice2 = Convert.ToChar(Console.ReadLine());
                if (choice2 == 'y')
                {
                    deposit();
                }
                if (choice2 == 'n')
                {
                    showMainMenu();
                }
            }


            Console.WriteLine("\n\n Account found! Enter the amount...");
            Console.SetCursorPosition(14, 6);
            //StreamReader sr = new StreamReader(path);
            string[] infor = File.ReadAllLines(path);
            String balance = infor[infor.Length - 1];
            infor[infor.Length - 1] = (Double.Parse(balance) - Convert.ToDouble(Console.ReadLine())).ToString();
            StreamWriter sw = new StreamWriter(path);
            foreach (string detail in infor)
            {
                sw.WriteLine(detail);
            }
            sw.Close();

            Console.WriteLine("\n\n Withdraw successfull!");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                showMainMenu();
            }
        }

        //5. A/C statement
        public void displayAccount(string account_number, double balance, string firstname, string lastname, string address, string phone, string email)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("|               STATEMENT                |");
            Console.WriteLine("|════════════════════════════════════════|");
            Console.WriteLine("|             ENTER THE DETAILS          |");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|    Account Number:                     |");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.SetCursorPosition(21, 5);
            if (account_number == null) {
                account_number = Console.ReadLine();
            }
                
            int outInt3;
            if (!Int32.TryParse(account_number, out outInt3) || account_number.Length > 10)
            {
                Console.WriteLine("\n\nThe account number must be integer or not more than 10 characters");
                Console.ReadKey();
                displayAccount(null,0.00,null,null,null,null,null);
            }
            
            //if file no exit
            string path = @"C:\Users\shan\Desktop\bank\" + account_number + ".txt";
            while (!System.IO.File.Exists(path))
            {
                Console.WriteLine("\n\nAccount not found!");
                Console.WriteLine("Check another account (y/n)? ");
                Console.SetCursorPosition(29, 9);
                char choice1 = Convert.ToChar(Console.ReadLine());
                if (choice1 == 'y')
                {
                    displayAccount(null,0.00,null,null,null,null,null);
                }
                if (choice1 == 'n')
                {
                    showMainMenu();
                }
            }

            Console.WriteLine("\n\nAccount found! The statement is displayed below...");
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("|          SIMPLE BANKING SYSTEM         |");
            Console.WriteLine("|════════════════════════════════════════|");
            Console.WriteLine("|    Account Statement                   |");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|    Account No:                         |");
            Console.WriteLine("|    Account Balance:                    |");
            Console.WriteLine("|    First Name:                         |");
            Console.WriteLine("|    Last Name:                          |");
            Console.WriteLine("|    Address:                            |");
            Console.WriteLine("|    Phone:                              |");
            Console.WriteLine("|    Email:                              |");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.SetCursorPosition(17, 14);
            Console.WriteLine(account_number);

            //get the file contents
            System.IO.StreamReader reader = new System.IO.StreamReader(path);

            for (int i = 0; i < 6; i++)
            {
                int left = 0;
                int top = 0;
                string line = reader.ReadLine();
                if (i == 0) { left = 17;top = 16; firstname = line; }
                if (i == 1) { left = 16; top = 17; lastname = line; }
                if (i == 2) { left = 14; top = 18; address = line; }
                if (i == 3) { left = 12; top = 19; phone = line; }
                if (i == 4) { left = 12; top = 20; email = line; }
                if (i == 5) { left = 21; top = 15; balance = Double.Parse(line); }
                Console.SetCursorPosition(left, top);
                Console.WriteLine(line);

            }
            reader.Close();


            Console.Write("\n\n\n\n\n\nEmail statement (y/n)?");
            char choice2 = Convert.ToChar(Console.ReadLine());
            if (choice2 == 'y')
            {
                Console.WriteLine("Email sent successfully!...");
                //send account to email
                sendAccountStatement( account_number,  balance,  firstname,  lastname,  address,  phone,  email);

            }
            else
            {
                //go back to main menu
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    showMainMenu();
                }
            }
        }

        // method sendAccountStatement
        public void sendAccountStatement(string account_number, double balance, string firstname, string lastname, string address, string phone, string email)
        {
            try
            {
                MailMessage mailMesseage = new MailMessage();
                MailAddress fromma = new MailAddress("yinshan19971209@gmail.com");
                MailAddress Toma = new MailAddress(email);
                mailMesseage.From = fromma;
                mailMesseage.To.Add(Toma);
                mailMesseage.Subject = "Account Information";
                mailMesseage.IsBodyHtml = false;
                mailMesseage.Body ="Account No: " + account_number +"\nBalance: " + balance + "FirstName: " + firstname + "\nLastName: " + lastname + "\nAddress: " + address + "\nPhone: " + phone + "\nemail: " + email;//邮件内容   
                mailMesseage.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
                mailMesseage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                //mailMesseage.CC.Add(Toma);
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;

                    client.Credentials = new NetworkCredential("yinshan19971209@gmail.com", "201313ys@");
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(mailMesseage);
                }

                MessageBox.Show("Send successful");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show(ex.Message, "Unsuccessful");
            }
        }
        
        //6. deleteAccount
        public void deleteAccount()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("|            DELETE AN ACCOUNT           |");
            Console.WriteLine("|════════════════════════════════════════|");
            Console.WriteLine("|            ENTER THE DETAILS           |");
            Console.WriteLine("|                                        |");
            Console.WriteLine("|    Account Number:                     |");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.SetCursorPosition(21, 5);
            string account_number = Console.ReadLine();

            int outInt4;
            if (!Int32.TryParse(account_number, out outInt4) || account_number.Length > 10)
            {
                Console.WriteLine("\n\nThe account number must be integer or not more than 10 characters");
                Console.ReadKey();
                deleteAccount();
            }
            //if file no exit
            string path = @"C:\Users\shan\Desktop\bank\" + account_number + ".txt";
            while (!System.IO.File.Exists(path))
            {
                Console.WriteLine("\n\nAccount not found!");
                Console.WriteLine("Delete another account (y/n)? ");
                Console.SetCursorPosition(29, 9);
                char choice1 = Convert.ToChar(Console.ReadLine());
                if (choice1 == 'y')
                {
                    deleteAccount();
                }
                if (choice1 == 'n')
                {
                    showMainMenu();
                }

            }
            //if file exit
            Console.WriteLine("\n\nAccount found! Details displayed below...");
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("|             ACCOUNT DETAILS            |");
            Console.WriteLine("|════════════════════════════════════════|");
            Console.WriteLine("|    Account No:                         |");
            Console.WriteLine("|    Account Balance:                    |");
            Console.WriteLine("|    First Name:                         |");
            Console.WriteLine("|    Last Name:                          |");
            Console.WriteLine("|    Address:                            |");
            Console.WriteLine("|    Phone:                              |");
            Console.WriteLine("|    Email:                              |");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.SetCursorPosition(17, 12);
            Console.WriteLine(account_number);
            //get the file contents
            System.IO.StreamReader reader = new System.IO.StreamReader(path);

            for (int i = 0; i < 6; i++)
            {
                int left = 0;
                int top = 0;
                if (i == 0)
                {
                    left = 17;
                    top = 14;
                }
                if (i == 1) { left = 16; top = 15; }
                if (i == 2) { left = 14; top = 16; }
                if (i == 3) { left = 12; top = 17; }
                if (i == 4) { left = 12; top = 18; }
                if (i == 5) { left = 21; top = 13; }
                Console.SetCursorPosition(left, top);
                string line = reader.ReadLine();
                Console.WriteLine(line);

            }
            reader.Close();

            Console.Write("\n\n\n\n\n\nDelte (y/n)?");
            char choice2 = Convert.ToChar(Console.ReadLine());
            if (choice2 == 'y')
            {
                //function delete account
                File.Delete(path);
                Console.WriteLine("Account Deleted!...");
                Console.ReadKey();
                showMainMenu();
            }
            else
            {
                //go back to main menu
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    showMainMenu();
                }
            }
        }

        //7.
        public void exit()
        {
            Environment.Exit(0);
        }

    }
}
