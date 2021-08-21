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

        public MainMenu(IReviewsBL rvsbL)
        {
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

            string[] choices = new string[] { "[0] Sign up", "[1] Find A User", "[2] View All Users", "[3] View All Restraunts", "[4] View All Reviews", "[5] Search Restaurant By Name", "[6] Add A Restraunt", "[7] Write a Review", "[8] Delete User", "[9] Exit" };

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
                        SignUp();
                        break;
                    case "1":
                        FindUsersByIdUI();
                        break;
                    case "2":
                        Console.WriteLine("You are viewing all of the members\n ---------- \n");
                        SeeAllMembers(); // done
                        break;
                    case "3":
                        SeeAllRestaurants(); // done
                        break;
                    case "4":
                        SeeAllReviews(); // done
                        break;
                    case "5":
                        SeeRestrauntByName(); // done
                        break;
                    case "6":
                        CreateRestaurant(); // done, need to return id
                        break;
                    case "7":
                        CreateReview();
                        break;
                    case "8":
                        DeleteUser();
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

        // private Member FindMember(List<Member> members, string prompt)
        // {
        //     Console.WriteLine(prompt);
        //     Console.WriteLine("Please enter user's username: ");



        //     Console.WriteLine("This is the user you're looking for: ");
        // }


        private void SignUp()
        {
            Member userToAdd = new Member();

            do
            {
                Console.Write("Please enter your FIRST name: ");
                userToAdd.FirstName = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(userToAdd.FirstName));

            do
            {
                Console.Write("Please enter your LAST name: ");
                userToAdd.LastName = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(userToAdd.LastName));

            do
            {
                Console.Write("Please enter your username: ");
                userToAdd.Username = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(userToAdd.Username));

            do
            {
                Console.Write("Please enter your email: ");
                userToAdd.Email = Console.ReadLine();
                // RegexUtilities.IsValidEmail(newUser.Email); 
            } while (String.IsNullOrWhiteSpace(userToAdd.Email));

            //TODO: NEED TO CHECK EMAIL AGAINST DATABASE

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

            userToAdd.Password = password1;

            // Console.Write("Is this User an Admin?");
            userToAdd.IsAdmin = 0;

            try
            {
                userToAdd = _userbl.AddUser(userToAdd);

                Console.WriteLine("Member Created!\n \n");
                Console.WriteLine($"ID: {userToAdd.Id}, Name: {userToAdd.FirstName} {userToAdd.LastName}, Email: {userToAdd.Email}, Username: {userToAdd.Username}, Admin: {userToAdd.IsAdmin}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"This is the error: {ex}. Please make changes accordingly and try again!");
            }
        }

        private void CreateRestaurant()
        {
            Restaurants restaurantToAdd = new Restaurants();
            Console.WriteLine("\nYou're adding a new restaurant!\n");

            do
            {
                Console.Write("Please enter the restaurant name: ");
                restaurantToAdd.Name = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(restaurantToAdd.Name));

            do
            {
                Console.Write("Please enter the location. Format: Address, City, State): ");
                restaurantToAdd.Location = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(restaurantToAdd.Location));

            int zipcode = 0;
            while (zipcode <= 10000)
            {
                Console.WriteLine("Please enter the zip code: ");
                try
                {
                    zipcode = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}, You didn't enter only numbers. Please try again.", ex);
                }

            }
            restaurantToAdd.ZipCode = zipcode;

            try
            {
                restaurantToAdd = _restaurantbl.AddRestaurant(restaurantToAdd);
                Console.WriteLine("Member Created!\n \n");
                Console.WriteLine($"ID: {restaurantToAdd.Id}, Name: {restaurantToAdd.Name}, Location: {restaurantToAdd.Location} {restaurantToAdd.ZipCode}\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"This is the error: {ex}. Please make changes accordingly and try again!");
            }
        }

        private void CreateReview()
        {
            Console.WriteLine("Please enter your review details below: ");

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


        private void SeeAllMembers()
        {
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

        private Restaurants SeeRestrauntByName()
        {
            string input;

            do
            {
                Console.WriteLine("Enter the name of the restaurant to search:");
                input = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(input));

            Restaurants foundRestaurant = _restaurantbl.SearchRestaurantByName(input);

            if (foundRestaurant.Name is null)
            {
                Console.WriteLine($"Sorry, {input} wasn't found. Please try again.");
            }
            else
            {
                Console.WriteLine("We found {0}!", foundRestaurant.Name);
            }

            return foundRestaurant;
        }

        private Member FindUsersByIdUI()
        {
            int input = -1;
            while (input < 0)
            {
                Console.WriteLine("Please enter a user's ID (numbers only): ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0}, You didn't enter only numbers. Please try again.", ex);
                }

            }
            Member foundUser = _userbl.SearchUserById(input);

            if (foundUser.Username is null)
            {
                Console.WriteLine($"Sorry, {input} wasn't found. Please try again.");
            }
            else
            {
                Console.WriteLine("We found {0}! ID: {1}", foundUser.Username, foundUser.Id);
            }

            return foundUser;
        }




        private void DeleteUser()
        {
            Console.WriteLine("Delete User Requested: ");

        }

        private static void ShutDown()
        {
            shutDownRequested = true;
        }
    }

}
