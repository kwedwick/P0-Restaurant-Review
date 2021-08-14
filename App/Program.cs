using System;
using Lib;
using Models;
using Data;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateMember();
        }

        static void CreateMember()
        {
            Member newUser = new Member();
            Console.Write("Please enter your username: ");
            newUser.Username = Console.ReadLine();
            
            Console.Write("Please enter your email ");
            newUser.Email = Console.ReadLine();

            string password1;
            string password2;
            do
            {
                do
                {
                    Console.Write("Please create a password: ");
                    password1 = Console.ReadLine();
                } while (password1 == "");

                do
                {
                    Console.Write("Please re-enter your password: ");
                    password2 = Console.ReadLine();
                } while (password1 == "");

                if (password1 != password2) Console.WriteLine("Passwords don't match. Please re-enter your password.");
                else continue;

            } while (password1 != password2);

            newUser.Password = password1;

            newUser.Role = "Member";

            Console.WriteLine(newUser.Username);

        }

    }


}