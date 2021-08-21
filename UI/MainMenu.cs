using Models;
using BL;
using System;
using System.Collections.Generic;


namespace UI
{
    public class MainMenu : IMenu
    {
        private IUsersBL _userbl;

        private IRestaurantsBL _restaurantbl;

        private IReviewsBL _reviewsbl;

        public MainMenu(IUsersBL ubL)
        {
            _userbl = ubL;
        }

        public MainMenu(IReviewsBL rvsbL) {
            _reviewsbl = rvsbL;
        }
        public MainMenu(IUsersBL ubL, IRestaurantsBL rbL, IReviewsBL rvsbL)
        {
            _userbl = ubL;
            _restaurantbl = rbL;
            _reviewsbl = rvsbL;
        }
        static bool shutDownRequested = false;
        public void Start()
        {

            Console.WriteLine("Welcome to Restaurant Reviewer!\nPlease choose one of the following options below by entering the corresponding number: ");

            string[] choices = new string[] { "[0] Create User", "[1] Find A User", "[2] View All Users", "[3] View All Restraunts", "[4] View All Reviews", "[5] Search Restaurant By Name", "[6] Add A Restraunt", "[7] Write a Review", "[8] Placeholder", "[9] Exit" };

            do
            {
                foreach (string choice in choices)
                {
                    Console.WriteLine($"{choice}");
                }

                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "0":
                        CreateMember();
                        break;
                    case "1":
                        FindMember(); // this is a placeholder function
                        // var user = await FindUser();
                        // Console.WriteLine($"{user.Email}, {user.Id}");
                        break;
                    case "2":
                        SeeAllMembers();
                        break;
                    case "3":
                        SeeAllRestaurants();
                        break;
                    case "4":
                        SeeAllReviews();
                        break;
                    case "5":
                        SeeRestrauntByName();
                        break;
                    case "6":
                        CreateRestaurant();
                        break;
                    case "7":
                        CreateReview();
                        break;
                    case "8":
                        // Placeholder
                        break;
                    case "9":
                        ShutDown();
                        break;
                    default:
                        Console.WriteLine("Please type your answer correctly!");
                        break;
                }
            } while (shutDownRequested == false);
        }


        private static void CreateMember(byte isAdmin = 0)
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
                } while (String.IsNullOrWhiteSpace(password1));

                do
                {
                    Console.Write("Please re-enter your password: ");
                    password2 = Console.ReadLine();
                } while (String.IsNullOrWhiteSpace(password1));

                if (password1 != password2) Console.WriteLine("Passwords don't match. Please re-enter your password.");
                else continue;

            } while (password1 != password2);

            newUser.Password = password1;

            // Console.Write("Is this User an Admin?");
            newUser.IsAdmin = isAdmin;

            Console.WriteLine(newUser.Username);

        }

        // private static async Task<Member> FindUser()
        // {
        //     int UserId = -1;
        //     do
        //     {
        //         Console.WriteLine("What is the ID? Only Integers: ");
        //     } while (int.TryParse(Console.ReadLine(), out UserId) == false);

        //     // return await UserRepo.FindUserById(UserId);
        // }

        private void FindMember()
        {
            Console.WriteLine("This is the user you're looking for: ");
        }

        private void SeeAllMembers()
        {
            Console.WriteLine("You are viewing all of the members\n ---------- \n");
            List<Member> members = _userbl.ViewAllUsers();

            foreach (Member member in members)
            {
                Console.WriteLine($"ID: {member.Id}, Name: {member.FirstName} {member.LastName}, Email: {member.Email}, Username: {member.Username}, Admin: {member.IsAdmin}\n");
            }
        }

        private void SeeAllRestaurants()
        {
            Console.WriteLine("You are viewing all of the restaurants\n ---------- \n");
            List<Restaurants> restaurants = _restaurantbl.ViewAllRestaurants();
            foreach (Restaurants restaurant in restaurants)
            {
                Console.WriteLine($"ID: {restaurant.Id} Name:{restaurant.Name} Location: {restaurant.Location} Zip Code: {restaurant.ZipCode}");
            }
        }
        private void SeeAllReviews()
        {
            Console.WriteLine("You are viewing all of the reviews\n ---------- \n");
            List<Review> reviews = _reviewsbl.ViewAllReviews();
            foreach (Review review in reviews)
            {
                Console.WriteLine($"ID: {review.Id} Created: {review.TimeCreated} Title: {review.Title} Post: {review.Body} Rating: {review.Rating}");
            }

        }

        private void SeeRestrauntByName()
        {
            Console.WriteLine("Here is your restraunt: ");
        }

        private void CreateRestaurant()
        {
            Console.WriteLine("Please enter restaurant details: ");
        }

        private void CreateReview()
        {
            Console.WriteLine("Please enter your review details below: ");
        }

        private static void ShutDown()
        {
            shutDownRequested = true;
        }
    }

}
