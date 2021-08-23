using Models;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;


namespace UI
{
    public class MainMenu : IMenu
    {
        /// <summary>
        /// calling the BL layers for users/restaurants and reviewslayer
        /// </summary>
        private IUsersBL _userbl;

        private IRestaurantsBL _restaurantbl;

        private IReviewsBL _reviewsbl;

        /// <summary>
        /// CurrentSession aka logged in user? is instantiated
        /// IsLoggedIn is how we filter the commands
        /// </summary>
        private Session _currentSession;

        public bool IsLoggedIn => _currentSession.CurrentUser is not null;

        private List<Commands> AllCommands = new();

        /// <summary>
        /// the MainMenu constructer that is taking in all of the layers
        /// </summary>
        /// <param name="ubL"></param>
        /// <param name="rbL"></param>
        /// <param name="rvsbL"></param>
        /// <param name="session"></param>
        public MainMenu(IUsersBL ubL, IRestaurantsBL rbL, IReviewsBL rvsbL, Session session)
        {
            _userbl = ubL;
            _restaurantbl = rbL;
            _reviewsbl = rvsbL;
            _currentSession = session;
            BuildCommandList();
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .WriteTo.File("../logs/restaurantviewerlogs.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
            Log.Information("UI begining");
        }
        static bool shutDownRequested = false;

        /// <summary>
        /// creating the commands that we filter to the user depending on if they are: logged in and member/admin
        /// </summary>
        public class Commands
        {
            public string? CommandName { get; set; }
            public string? Command { get; set; }
            public Action? Execution { get; set; }
            public bool MustBeAdmin { get; set; }
            public bool MustBeLoggedIn { get; set; }
        }


        /// <summary>
        /// The command list that users select. We filter based on the MustBeAdmin and MustBeLoggedIn
        /// </summary>
        public void BuildCommandList()
        {
            AllCommands = new List<Commands>(){
                new Commands(){
                    CommandName = "[0] Login",
                    Command = "0",
                    Execution = Login,
                    MustBeAdmin = false,
                    MustBeLoggedIn = false
                },
                new Commands(){
                    CommandName = "[1] Sign up!",
                    Command = "1",
                    Execution = SignUp,
                    MustBeAdmin = false,
                    MustBeLoggedIn = false
                },
                new Commands(){
                    CommandName = "[2] View All Restaurants",
                    Command = "2",
                    Execution = SeeAllRestaurants,
                    MustBeAdmin = false,
                    MustBeLoggedIn = false
                },
                new Commands(){
                    CommandName = "[3] Search Restaurant by Name",
                    Command = "3",
                    Execution = SeeRestrauntByName,
                    MustBeAdmin = false,
                    MustBeLoggedIn = false
                },
                new Commands(){
                    CommandName = "[4] Shut Down",
                    Command = "4",
                    Execution = ShutDown,
                    MustBeAdmin = false,
                    MustBeLoggedIn = false,
                },
                new Commands(){
                    CommandName = "[5] Find A User",
                    Command = "5",
                    Execution = FindUsersByIdUI,
                    MustBeAdmin = true,
                    MustBeLoggedIn = true
                },
                new Commands(){
                    CommandName = "[6] View All Users",
                    Command = "6",
                    Execution = SeeAllMembers,
                    MustBeAdmin = true,
                    MustBeLoggedIn = true
                },
                new Commands(){
                    CommandName = "[7] See All Reviews",
                    Command = "7",
                    Execution = SeeAllReviews,
                    MustBeAdmin = false,
                    MustBeLoggedIn = true
                },
                new Commands(){
                    CommandName = "[8] Create A Review ",
                    Command = "8",
                    Execution = CreateReviewUI,
                    MustBeAdmin = false,
                    MustBeLoggedIn = true,
                },
                new Commands(){
                    CommandName = "[9] Delete User",
                    Command = "9",
                    Execution = DeleteUser,
                    MustBeAdmin = true,
                    MustBeLoggedIn = true
                },
                new Commands(){
                    CommandName = "[10] View Reviews by Restaurat",
                    Command = "10",
                    Execution = ViewReviewsByRestaurant,
                    MustBeAdmin = false,
                    MustBeLoggedIn = false
                },
                // new Commands(){
                //     CommandName = "[] ",
                //     Command = "",
                //     Execution = ,
                //     MustBeAdmin = false,
                //     MustBeLoggedIn = false
                // },

            };
        }

        /// <summary>
        /// It's taking in user's selection of the command and checking if it's acceptable
        /// /// /// </summary>
        public bool RunCommand(string? Input)
        {



            /// checks if the command was acceptable
            var command = AllCommands.First(i => i.Command == Input);
            if (command is null)
            {
                Log.Error("Invalid Command.  Try Again.");
                return false;
            }
            if (command?.Execution is null)
            {
                Log.Error("System error");
                return false;
            }
            /// runs the assigned function per the user selected command
            command.Execution.Invoke();
            return true;
        }

        /// <summary>
        /// This is what filters the list based on logged in and member/admin. this is called after each command finishes until the users tells the application to shut down
        /// </summary>
        public void PrintAllValidCommandOptions()
        {

            if ((IsLoggedIn == true) && (_currentSession.CurrentUser?.IsAdmin == 1))
            {
                List<Commands> adminCommands = AllCommands.ToList();
                foreach (var i in adminCommands)
                {
                    Console.WriteLine(i.CommandName);
                }
            }
            else if ((IsLoggedIn == true) && (_currentSession.CurrentUser?.IsAdmin == 0))
            {
                List<Commands> memberCommands = AllCommands
                .Where(i => i.MustBeAdmin == (_currentSession.CurrentUser?.IsAdmin == 1))
                .ToList();
                foreach (var i in memberCommands)
                {
                    Console.WriteLine(i.CommandName);
                }
            }
            else
            {
                List<Commands> validCommands = AllCommands
            .Where(i => i.MustBeLoggedIn == IsLoggedIn)
            .Where(i => i.MustBeAdmin == (_currentSession.CurrentUser?.IsAdmin == 1))
            .ToList();
                foreach (var i in validCommands)
                {
                    Console.WriteLine(i.CommandName);
                }
            }
            // Get all valid commands that can be run from AllCommands, based on is logged in and is admin
            // ordered from generic to specific




        }

        /// <summary>
        /// Starts the CLI application
        /// </summary>
        public void Start()
        {


            Console.WriteLine("Welcome to Restaurant Reviewer!\nPlease choose one of the following options below by entering the corresponding number: ");

            do
            {
                PrintAllValidCommandOptions();
                string startingInput;
                do
                {
                    startingInput = Console.ReadLine();
                } while (String.IsNullOrWhiteSpace(startingInput));

                RunCommand(startingInput);
                // switch (startingInput)
                // {
                //     case "0":
                //         Login();
                //         break;
                //     case "1":
                //         SignUp();
                //         break;
                //     case "2":
                //         SeeAllRestaurants();
                //         break;
                //     case "3":
                //         ShutDown();
                //         break;
                //     default:
                //         Console.WriteLine("Please type your answer correctly!");
                //         break;
                // }

            } while (shutDownRequested == false);

            // shutDownRequested = true;

            // string[] choices = new string[] { 
            //     "[0] Sign up", 
            //     "[1] Find A User", 
            //     "[2] View All Users", 
            //     "[3] View All Restraunts", 
            //     "[4] View All Reviews", 
            //     "[5] Search Restaurant By Name", 
            //     "[6] Add A Restraunt", 
            //     "[7] Write a Review", 
            //     "[8] Delete User", 
            //     "[9] Exit" };

            // do
            // {
            //     foreach (string choice in choices)
            //     {
            //         Console.WriteLine($"{choice}");
            //     }

            //     string userInput = Console.ReadLine();
            //     switch (userInput)
            //     {
            //         case "0":
            //             SignUp();
            //             break;
            //         case "1":
            //             FindUsersByIdUI();
            //             break;
            //         case "2":
            //             Console.WriteLine("You are viewing all of the members\n ---------- \n");
            //             SeeAllMembers(); // done
            //             break;
            //         case "3":
            //             SeeAllRestaurants(); // done
            //             break;
            //         case "4":
            //             SeeAllReviews(); // done
            //             break;
            //         case "5":
            //             SeeRestrauntByName(); // done
            //             break;
            //         case "6":
            //             CreateRestaurant(); // done, need to return id
            //             break;
            //         case "7":
            //             CreateReviewUI();
            //             break;
            //         case "8":
            //             DeleteUser();
            //             break;
            //         case "9":
            //             ShutDown();
            //             break;
            //         default:
            //             Console.WriteLine("Please type your answer correctly!");
            //             break;
            //     }
            // } while (shutDownRequested == false);
        }

        // private Member FindMember(List<Member> members, string prompt)
        // {
        //     Console.WriteLine(prompt);
        //     Console.WriteLine("Please enter user's username: ");



        //     Console.WriteLine("This is the user you're looking for: ");
        // }

        /// <summary>
        /// Creates a new user
        /// </summary>
        private void SignUp()
        {
            if (IsLoggedIn == true)
            {
                Log.Error("You are already signed up and logged in!");
                return;
            }
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

            /// <summary>
            /// make sure the password is the same and not empty
            /// </summary>
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

                if (password1 != password2) Log.Error("Passwords don't match. Please re-enter your password.");
                else continue;

            } while (password1 != password2);

            userToAdd.Password = password1;

            // Console.Write("Is this User an Admin?");
            userToAdd.IsAdmin = 0;

            /// <summary>
            /// trying to add the user to db and handle the exception
            /// </summary>
            /// <value></value>

            try
            {
                userToAdd = _userbl.AddUser(userToAdd);

                Console.WriteLine("Member Created!\n \n");
                Console.WriteLine($"ID: {userToAdd.Id}, Name: {userToAdd.FirstName} {userToAdd.LastName}, Email: {userToAdd.Email}, Username: {userToAdd.Username}, Admin: {userToAdd.IsAdmin}\n");
                _currentSession.CurrentUser = userToAdd;
            }
            catch (Exception ex)
            {
                Log.Error($"This is the error: {ex}.\nPlease make changes accordingly and try again!");
            }
        }

        /// <summary>
        /// logs in the user
        /// </summary>
        private void Login()
        {
            if (IsLoggedIn == true)
            {
                Log.Error("You are already logged in!");
                return;
            }
            Member checkUser = new Member();
            do
            {
                Console.Write("Please enter your username: ");
                checkUser.Username = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(checkUser.Username));

            string password;
            do
            {
                Console.Write("Please enter a password: ");
                password = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(password));
            checkUser.Password = password;
            checkUser = _userbl.CheckUserLogin(checkUser);


            /// make suure rhe user was found and not empty
            if (checkUser.Username != null)
            {
                try
                {
                    Console.WriteLine("Welcome {0}!", checkUser.Username);
                    _currentSession.CurrentUser = checkUser;
                }
                catch (Exception ex)
                {
                    Log.Error("{0},\n There was an error with your login. User not found or wrong username/password. Please try agaiin.", ex);
                }
            }
            else
            {
                Log.Debug("User not found. Select a new option.");
            }
        }

        /// <summary>
        /// Creating a new restaurant from a logged in user
        /// </summary>
        private void CreateRestaurant()
        {
            if (IsLoggedIn == false)
            {
                Log.Error("You must be logged in to do this!");
                return;
            }
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
                Log.Error($"This is the error: {ex}. Please make changes accordingly and try again!");
            }
        }
        /// <summary>
        /// A logged in user creating a Review
        /// </summary>
        private void CreateReviewUI()
        {
            if (IsLoggedIn == false)
            {
                Log.Error("You must be logged in to do this!");
                return;
            }
            CreateReview newReview = new CreateReview();

            newReview.UserId = _currentSession.CurrentUser.Id;

            List<Restaurants> restaurants = _restaurantbl.ViewAllRestaurants();
            Restaurants foundRestaurant = SelectAReviewByRestaurantIdUI(restaurants, "Pick a restaurant by entering the corresponding [number]: ");
            newReview.RestaurantId = foundRestaurant.Id;


            newReview.TimeCreated = DateTime.Now;
            Console.WriteLine("Please enter your review details below: ");

            string title;
            do
            {
                Console.WriteLine("Enter the Title of your review:");
                title = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(title));
            newReview.Title = title;

            string body;
            do
            {
                Console.WriteLine("Enter the Body of your review:");
                body = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(body));
            newReview.Body = body;

            int userRating = 0;
            Console.WriteLine("Please enter your Rating 1-5: ");
            do
            {
                try
                {
                    userRating = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Log.Error("{0}, You didn't enter only numbers or a number between 1-5. Please try again.", ex);
                }
                // if(userRating < 1 || userRating > 5) {
                //     userRating = 0;
                //     Console.WriteLine
                // }
                Console.WriteLine("You must enter a number between 1-5");

            } while (userRating < 1 || userRating > 5);
            newReview.Rating = userRating;

            try
            {
                newReview = _reviewsbl.AddReview(newReview);
                Console.WriteLine("Review Created!\n \n");
                Console.WriteLine($"Review ID: {newReview.Id}, Title: {newReview.Title}, Body: {newReview.Body}\n Rating: {newReview.Rating}\n UserID: {newReview.UserId}");

            }
            catch (Exception ex)
            {
                Log.Error($"This is the error: {ex}. Please make changes accordingly and try again!");
            }


        }

        /// <summary>
        /// An Admin can see all members in the database
        /// </summary>
        private void SeeAllMembers()
        {
            if (IsLoggedIn == false)
            {
                Log.Error("You must be logged in and an admin to do this!");
                return;
            }
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

        /// <summary>
        /// Returns all reviews in the database. Must be logged in and an admin
        /// </summary>
        private void SeeAllReviews()
        {
            if (IsLoggedIn == false)
            {
                Log.Error("You must be logged in and an admin to do this!");
                return;
            }
            Console.WriteLine("You are viewing all of the reviews\n ---------- \n");
            List<Review> reviews = _reviewsbl.ViewAllReviews();
            foreach (Review review in reviews)
            {
                Console.WriteLine($"ID: {review.Id} Created: {review.TimeCreated} Title: {review.Title} Post: {review.Body} Rating: {review.Rating}");
            }

        }

        /// <summary>
        /// User enters a string input to match if the restaurant is found. Any one can run this function
        /// </summary>
        private void SeeRestrauntByName()
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
                Log.Error($"Sorry, {input} wasn't found. Please try again.");
            }
            else
            {
                Console.WriteLine("We found {0}!", foundRestaurant.Name);
            }

            Console.WriteLine(foundRestaurant);
        }

        /// <summary>
        /// User selects a restaurat from a list and returns reviews based on that selection. Anyone can use.
        /// </summary>
        private void ViewReviewsByRestaurant()
        {
            List<Restaurants> restaurants = _restaurantbl.ViewAllRestaurants();

            Restaurants foundRestaurant = SelectAReviewByRestaurantIdUI(restaurants, "Pick a restaurant by entering the corresponding [number]: ");

            List<RestaurantReviews> reviews = _reviewsbl.GetReviewsbyRestaurantIdBL(foundRestaurant.Id);

            decimal sum = 0;
            int n = 0;
            for (int i = 0; i < reviews.Count; i++)
            {
                sum += Convert.ToDecimal(reviews[i].Rating);
                n += 1;
            }
            decimal average = (sum / n);
            decimal average1 = Math.Round(average, 2);

            Console.WriteLine($"Here are the reviews for: {foundRestaurant.Name}");
            Console.WriteLine($"Averge Rating: {average1}");
            foreach (RestaurantReviews review in reviews)
            {
                Console.WriteLine($"Title: {review.Title}\n\tBody: {review.Body}\n\tRating:{review.Rating}\n\tBy:{review.Username}\n");
            }
        }
        private Restaurants SelectAReviewByRestaurantIdUI(List<Restaurants> restaurants, string prompt)
        {
            Console.WriteLine(prompt);
            int selection;
            bool valid = false;

            do
            {
                for (int i = 0; i < restaurants.Count; i++)
                {
                    Console.WriteLine($"[{i}] {restaurants[i].Name}");
                }

                valid = int.TryParse(Console.ReadLine(), out selection);

                //this means that the parsing to integer has been successful and is within list's range
                if (valid && (selection >= 0 && selection < restaurants.Count))
                {
                    return restaurants[selection];
                }

                Console.WriteLine("Enter a valid number");
            } while (true);
        }

        /// <summary>
        /// Searches a User by ID. Must be an admin to use.
        /// </summary>
        private void FindUsersByIdUI()
        {
            if (IsLoggedIn == false)
            {
                Log.Error("You must be logged in and an admin to do this!");
                return;
            }
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
                Log.Error($"Sorry, {input} wasn't found. Please try again.");
            }
            else
            {
                Log.Debug("We found {0}! ID: {1}", foundUser.Username, foundUser.Id);
            }
        }

        /// <summary>
        /// Deletes a user from the database. Must be an admin to do so.
        /// </summary>
        private void DeleteUser()
        {
            if (IsLoggedIn == false)
            {
                Log.Error("You must be logged in and an admin to do this!");
                return;
            }

            Console.WriteLine("Delete User Requested: ");

        }

        /// <summary>
        /// Shuts down the program
        /// </summary>
        private static void ShutDown()
        {
            shutDownRequested = true;
        }
    }

}
