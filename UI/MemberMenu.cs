// using Models;
// using BL;
// using System;
// using System.Collections.Generic;

// namespace UI
// {
//     public class MemberMenu
//     {
//         static bool shutDownRequested = false;
//         string[] choices = new string[] { "[0] View All Restraunts", "[1] View Your Reviews", "[2] Search Restaurant By Name", "[3] Write a Review", "[4] Placeholder", "[5] Logout", "[6] Close Program" };

//         public void MemberMenuStart()
//         {
//              do
//             {
//                 foreach (string choice in choices)
//                 {
//                     Console.WriteLine($"{choice}");
//                 }

//                 string userInput = Console.ReadLine();
//                 switch (userInput)
//                 {
//                     case "0":
//                         ViewAllRestaurants();
//                         break;
//                     case "1":
//                         ViewYourReviews();
//                         break;
//                     case "2":
//                         ViewRestrauntByName();
//                         break;
//                     case "3":
//                         CreateReview();
//                         break;
//                     case "4":
//                         Placeholder();
//                         break;
//                     case "5":
//                         MemberLogout();
//                         break;
//                     case "6":
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