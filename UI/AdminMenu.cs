// using Models;
// using BL;
// using System;
// using System.Collections.Generic;

// namespace UI
// {
//     public class AdminMenu
//     {
//         static bool shutDownRequested = false;
//         string[] choices = new string[] { "[0] Create User", "[1] Find A User", "[2] View All Users", "[3] View All Restraunts", "[4] View All Reviews", "[5] Search Restaurant By Name", "[6] Add A Restraunt", "[7] Write a Review", "[8] Placeholder", "[9] Exit" };

//         public void AdminMenuStart()
//         {
//             do
//             {
//                 foreach (string choice in choices)
//                 {
//                     Console.WriteLine($"{choice}");
//                 }

//                 string userInput = Console.ReadLine();
//                 switch (userInput)
//                 {
//                     case "0":
//                         CreateMember();
//                         break;
//                     case "1":
//                         FindMember(); // this is a placeholder function
//                                       // var user = await FindUser();
//                                       // Console.WriteLine($"{user.Email}, {user.Id}");
//                         break;
//                     case "2":
//                         ViewAllMembers();
//                         break;
//                     case "3":
//                         ViewAllRestaurants();
//                         break;
//                     case "4":
//                         ViewAllReviews();
//                         break;
//                     case "5":
//                         ViewRestrauntByName();
//                         break;
//                     case "6":
//                         CreateRestaurant();
//                         break;
//                     case "7":
//                         CreateReview();
//                         break;
//                     case "8":
//                         // Placeholder
//                         break;
//                     case "9":
//                         ShutDown();
//                         break;
//                     default:
//                         Console.WriteLine("Please type your answer correctly!");
//                         break;
//                 }
//             } while (shutDownRequested == false);


//         }
//         private static void ShutDown()
//         {
//             shutDownRequested = true;
//         }
        
//     }
// }

