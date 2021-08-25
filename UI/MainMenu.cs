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

        // calling the BL layers for users/restaurants and reviewslayer
        private IUsersBL _userbl;

        private IRestaurantsBL _restaurantbl;

        private IReviewsBL _reviewsbl;

        // CurrentSession aka logged in user? is instantiated
        // IsLoggedIn is how we filter the commands
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
                    MustBeLoggedIn = true
                },
                new Commands(){
                    CommandName = "[3] Search Restaurant by Name",
                    Command = "3",
                    Execution = SeeRestrauntByName,
                    MustBeAdmin = false,
                    MustBeLoggedIn = true
                },
                new Commands(){
                    CommandName = "[4] See My Reviews",
                    Command = "4",
                    Execution = SeeMyReviews,
                    MustBeAdmin = false,
                    MustBeLoggedIn = true
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
                    CommandName = "[9] Create Restaurant",
                    Command = "9",
                    Execution = CreateRestaurant,
                    MustBeAdmin = false,
                    MustBeLoggedIn = true
                },
                new Commands(){
                    CommandName = "[10] View Reviews by Restaurant",
                    Command = "10",
                    Execution = ViewReviewsByRestaurant,
                    MustBeAdmin = false,
                    MustBeLoggedIn = true
                },
                new Commands(){
                    CommandName = "[11] Shut Down",
                    Command = "11",
                    Execution = ShutDown,
                    MustBeAdmin = false,
                    MustBeLoggedIn = false,
                },
                new Commands(){
                    CommandName = "[12] Logout",
                    Command = "12",
                    Execution = Logout,
                    MustBeAdmin = false,
                    MustBeLoggedIn = true,
                },

            };
        }

        /// <summary>
        /// It's taking in user's selection of the command and checking if it's acceptable
        /// /// /// </summary>
        public bool RunCommand(string? Input)
        {
            // checks if the command was acceptable
            var command = AllCommands.FirstOrDefault(i => i.Command == Input);

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
            // runs the assigned function per the user selected command
            command.Execution.Invoke();
            return true;
        }

        /// <summary>
        /// This is what filters the list based on logged in and member/admin. this is called after each command finishes until the users tells the application to shut down
        /// </summary>
        public void PrintAllValidCommandOptions()
        {
            // Logged in and Admin
            if ((IsLoggedIn == true) && (_currentSession.CurrentUser?.IsAdmin == 1))
            {
                List<Commands> adminCommands = AllCommands.ToList();
                foreach (var i in adminCommands)
                {
                    Console.WriteLine(i.CommandName);
                }
            }
            //logged in not admin
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
            // not logged in
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
        }

        /// <summary>
        /// Starts the CLI application
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Welcome to Restaurant Reviewer!\n");

            do
            {
                //When app starts, it displays not logged in commands
                PrintAllValidCommandOptions();
                Console.WriteLine("Please choose one of the following options below by entering the corresponding number: ");
                string startingInput;
                do
                {
                    //user then makes a selections
                    startingInput = Console.ReadLine();
                } while (String.IsNullOrWhiteSpace(startingInput));

                //then puts in the user input
                RunCommand(startingInput);

            } while (shutDownRequested == false);
        }

        /// <summary>
        /// This is how you create a user. It validates Email and Username as they must be unique.
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

            string checkingUsername;
            string createdUsername;
            do
            {
                do
                {
                    Console.Write("Please enter your username: ");
                    createdUsername = Console.ReadLine();
                } while (String.IsNullOrWhiteSpace(createdUsername));
                Console.WriteLine("Checking if username is unique....\n");
                checkingUsername = _userbl.CheckUniqueUsername(createdUsername);

                if (createdUsername == checkingUsername)
                {
                    Log.Error($"{createdUsername} was not unique! Try Again.");
                }

            } while (createdUsername == checkingUsername);
            Console.WriteLine("Username is unique!\n");
            userToAdd.Username = createdUsername;

            string checkingEmail;
            string createdEmail;
            do
            {
                do
                {
                    Console.Write("Please enter your email: ");
                    createdEmail = Console.ReadLine();
                } while (String.IsNullOrWhiteSpace(createdEmail));
                Console.WriteLine("Checking if email is unique....\n");
                checkingEmail = _userbl.CheckUniqueEmail(createdEmail);

                if (createdEmail == checkingEmail)
                {
                    Log.Error($"{createdEmail} was not unique! Try Again.");
                }

            } while (createdEmail == checkingEmail);
            Console.WriteLine("Email is unique!\n");
            userToAdd.Email = createdEmail;

            // make sure the password is the same and not empty
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
            userToAdd.IsAdmin = 0;

            // trying to add the user to db and handle the exception
            try
            {
                userToAdd = _userbl.AddUser(userToAdd);
                _currentSession.CurrentUser = userToAdd;

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
        /// Check if email is unique in the db
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private string CheckEmailIsUnique(string email)
        {
            return email = _userbl.CheckUniqueEmail(email);
        }
        /// <summary>
        /// Check if username is unique in the db
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string CheckUsernameIsUnique(string username)
        {
            return username = _userbl.CheckUniqueUsername(username);

        }

        /// <summary>
        /// Log in User to application
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


            // make suure rhe user was found and not empty
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
                Console.WriteLine("Restaurant Created!\n\n");
                Console.WriteLine($"ID: {restaurantToAdd.Id}\nName: {restaurantToAdd.Name}\nLocation: {restaurantToAdd.Location} {restaurantToAdd.ZipCode}\n");

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

            do
            {
                Console.WriteLine("Please enter your Rating between 1-5: ");
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
            if ((IsLoggedIn == false) && (_currentSession.CurrentUser?.IsAdmin == 0 || _currentSession.CurrentUser == null))
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

        /// <summary>
        /// User can view all restaurants in the database
        /// </summary>
        private void SeeAllRestaurants()
        {
            Console.WriteLine("You are viewing all of the restaurants\n ---------- \n");
            List<Restaurants> restaurants = _restaurantbl.ViewAllRestaurants();
            foreach (Restaurants restaurant in restaurants)
            {
                Console.WriteLine($"ID: {restaurant.Id} Name: {restaurant.Name}\nLocation: {restaurant.Location} {restaurant.ZipCode}\n");
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
                Console.WriteLine("\nWe found it!\n");
                Console.WriteLine($"\tID: {foundRestaurant.Id}\n\tName: {foundRestaurant.Name}\n\tLocation: {foundRestaurant.Location} {foundRestaurant.ZipCode}\n");

            }
        }

        /// <summary>
        /// User selects a restaurat from a list and returns reviews based on that selection. Anyone can use.
        /// </summary>
        private void ViewReviewsByRestaurant()
        {
            List<Restaurants> restaurants = _restaurantbl.ViewAllRestaurants();

            Restaurants foundRestaurant = SelectAReviewByRestaurantIdUI(restaurants, "Pick a restaurant by entering the corresponding [number]: ");
            int foundId = foundRestaurant.Id;
            List<RestaurantReviews> reviews = _reviewsbl.GetReviewsbyRestaurantIdBL(foundId);

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
        /// <summary>
        /// This prompts the user to select a restaurant from the list and returns the restaurant
        /// </summary>
        /// <param name="restaurants"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
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
            if ((IsLoggedIn == false) && (_currentSession.CurrentUser?.IsAdmin == 0 || _currentSession.CurrentUser == null))
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
        private void DeleteUserUI()
        {
            if ((IsLoggedIn == false) && (_currentSession.CurrentUser?.IsAdmin == 0 || _currentSession.CurrentUser == null))
            {
                Log.Error("You must be logged in and an admin to do this!");
                return;
            }
            Console.WriteLine("Delete User Requested: ");
            SeeAllMembers();
            Console.WriteLine("Please select the User ID to delete: ");

            int userIdInput = 0;
            do
            {
                Console.WriteLine("Please enter your Rating between 1-5: ");
                try
                {
                    userIdInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Log.Error("{0}, You didn't enter only numbers or a number between 1-5. Please try again.", ex);
                }
                // if(userRating < 1 || userRating > 5) {
                //     userRating = 0;
                //     Console.WriteLine
                // }

            } while (userIdInput == 0);
            Console.WriteLine("Thank you for selecting. Route currently under construction!");

        }

        private void SeeMyReviews()
        {
            if (IsLoggedIn == false)
            {
                Log.Error("You must be logged in and an admin to do this!");
                return;
            }

            int currentUserId = _currentSession.CurrentUser.Id;

            List<RestaurantReviews> myReviews = _reviewsbl.SeeMyReviews(currentUserId);

            if (myReviews != null)
            {
                foreach (RestaurantReviews review in myReviews)
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine($"Restaurant: {review.RestaurantName}\nTitle: {review.Title}\n\tBody: {review.Body}\n\tRating:{review.Rating}\n");
                }
            }
            else
            {
                Console.WriteLine("No reviews found :( Try writing some!");
            }


        }
        
        private void Logout()
        {
             if (IsLoggedIn == false)
            {
                Log.Error("You must be logged in and an admin to do this!");
                return;
            }
            string firstName = _currentSession.CurrentUser.FirstName;
            _currentSession.CurrentUser = null;
            Log.Debug($"You have successfully logged out! See you next time, {firstName}!");
        }

        /// <summary>
        /// Shuts down the program
        /// </summary>
        private static void ShutDown()
        {
            Log.Debug("Shutting down....Goodbye!");
            shutDownRequested = true;
        }
    }

}
