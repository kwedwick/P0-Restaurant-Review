using System;

namespace App
{
    public class MainMenu
    {
        public async Task DisplayMessage() {
            
            Console.WriteLine("AddUser, FindUserById, RestaurantDetails, SeeReviews, SearchRestaurant, Exit");
            string userInput = Console.ReadLine();
            
            await InterpretCommand(userInput);
        }
        private async Task InterpretCommand(string userInput) {


            switch (userInput)
            {   
                case "AddUser":
                    Program.CreateMember();
                    break;
                case "FindUserById":
                    var user = await Program.FindUser();
                    Console.WriteLine($"{user.Email}, {user.UserId}");
                    break;
                case "RestaurantDetails":
                    break;
                case "SeeReviews":
                    break;
                case "SearchRestaurant":
                    break;
                case "Exit":
                    Program.ShutDown();
                    break;
                default:
                    Console.WriteLine("Please type your answer correctly!");
                    break;
            }
        }
    }
}