using System;
using System.IO;

namespace Accounts.txt_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * This is an example file of how to use Amino.Net to split up a default accounts.txt file
             * This file goes over the formats: name:email:password:deviceId, email:password:device
             * 
             * For further questions feel free to join the official Amino.Net discord server that can be found on the GitHub page if you have any questions!
             */


            string account_Name = "name:email:password:deviceId";
            string account_NoName = "email:password:deviceId";


            //Single account (name:email:password:deviceId)
            SingleAccount_Name(account_Name);

            //Single account (email:password:deviceId)
            SingleAccount_NoName(account_NoName);


            //Get accounts from file

            //accounts.txt (name:email:password:deviceId)
            Get_from_File.AccountFromFile_Name("Your File Path");

            //accounts.txt (email:password:deviceId)
            Get_from_File.AccountFromFile_NoName("Your File Path");


            //Login Example

            //Login with single account (name:email:password:deviceId)
            Login_Example.Login_Name(account_Name);

            //Login with single account (email:password:deviceId)
            Login_Example.Login_NoName(account_NoName);

            /*
             * If you want to login with an accounts.txt file, you can just run a for each loop and refer to any of the login examples!
             */


        }


        //Single account (name:email:password:deviceId)
        static void SingleAccount_Name(string account)
        {
            string[] accountData = account.Split(':');
            Console.WriteLine(accountData[0]); //Name
            Console.WriteLine(accountData[1]); //Email
            Console.WriteLine(accountData[2]); //Password
            Console.WriteLine(accountData[3]); //DeviceId
        }

        //Single account (email:password:deviceId)
        static void SingleAccount_NoName(string account)
        {
            string[] accountData = account.Split(':');
            Console.WriteLine(accountData[0]); //Email
            Console.WriteLine(accountData[1]); //Password
            Console.WriteLine(accountData[2]); //DeviceId
        }
    }

    class Get_from_File
    {
        //accounts.txt (name:email:password:deviceId)
        public static void AccountFromFile_Name(string path)
        {
            foreach(string account in File.ReadAllLines(path))
            {
                string[] accountData = account.Split(':');
                Console.WriteLine(accountData[0]); //Name
                Console.WriteLine(accountData[1]); //Email
                Console.WriteLine(accountData[2]); //Password
                Console.WriteLine(accountData[3]); //DeviceId
                Console.WriteLine(string.Empty); //This line only seperates the account output
            }
        }

        //accounts.txt (email:password:deviceId)
        public static void AccountFromFile_NoName(string path)
        {
            foreach(string account in File.ReadAllLines(path))
            {
                string[] accountData = account.Split(':');
                Console.WriteLine(accountData[0]); //Email
                Console.WriteLine(accountData[1]); //Password
                Console.WriteLine(accountData[2]); //DeviceId
            }
        }
    }


    class Login_Example
    {
        //Login with single account (name:email:password:deviceId)
        public static void Login_Name(string account)
        {
            string[] accountData = account.Split(':');
            Amino.Client client = new Amino.Client(accountData[3]);


            Console.WriteLine("Logging in with account: " + accountData[0]); //This will print out the account name
            client.login(accountData[1], accountData[2]);
            Console.WriteLine("Logged in!");
        }

        //Login with single account (email:password:deviceId)
        public static void Login_NoName(string account)
        {
            string[] accountData = account.Split(':');
            Amino.Client client = new Amino.Client(accountData[2]);

            Console.WriteLine("Logging in with account: " + accountData[0]); //This will print out the account email
            client.login(accountData[0], accountData[1]);
            Console.WriteLine("Logged in!");
        }



    }
}
