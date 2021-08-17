using System;
using Lib;
using Models;
using Data;

namespace App
{
    class Program
    {
        static bool shutDownRequested = false;
        static void Main(string[] args)
        {
            var MainMenu = new MainMenu();
            Console.WriteLine("Welcome to Restaurant Reviewer!\nPlease choose one of the following options below: ");
            while (shutDownRequested == false)
            {
              MainMenu.DisplayMessage().Wait();  
            }
            
            
        }
        public static void ShutDown() {
            shutDownRequested = true; 
        }
        public static void CreateMember(bool isAdmin=false)
        {
            Member newUser = new Member();
            Console.Write("Please enter your username: ");
            newUser.Username = Console.ReadLine();
            
            Console.Write("Please enter your email: ");
            newUser.Email = Console.ReadLine();
            // RegexUtilities.IsValidEmail(newUser.Email);

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

            // Console.Write("Is this User an Admin?");
            newUser.IsAdmin = isAdmin;

            Console.WriteLine(newUser.Username);

        }

        public static async Task<Member> FindUser(){
            int UserId = -1;
            do{
              Console.WriteLine("What is the ID? Only Integers: ");
            } while (int.TryParse(Console.ReadLine(), out UserId) == false);
            
            return await UserRepo.FindUserById(UserId);
            

            
        }

    }


}