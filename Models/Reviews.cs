namespace Models
{
    /// <summary>
    /// Basic Review class that holds just the review table properties
    /// </summary>
    public class Review
    {
        /// <summary>
        /// This is the basic constructor
        /// </summary>
        public Review() { }

/// <summary>
/// Use this to get a review
/// </summary>
/// <param name="id"></param>
/// <param name="timeCreated"></param>
/// <param name="title"></param>
/// <param name="body"></param>
/// <param name="rating"></param>
        public Review(int id, System.DateTime timeCreated, string title, string body, int rating)
        {
            this.Id = id;
            this.TimeCreated = timeCreated;
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
        }
/// <summary>
/// Displaying basic review information
/// </summary>
/// <param name="title"></param>
/// <param name="body"></param>
/// <param name="rating"></param>
/// <returns></returns>
        public Review(string title, string body, int rating) : this()
        {
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
        }

        public int Id { get; set; }

        public System.DateTime TimeCreated { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int Rating { get; set; }
    }

/// <summary>
/// This class is for getting reviews for a specific restaurant from the db and displaying it to the user 
/// </summary>
    public class RestaurantReviews : Review
    {
        public RestaurantReviews() {}
/// <summary>
/// this can be returned from the db
/// </summary>
/// <param name="id"></param>
/// <param name="title"></param>
/// <param name="body"></param>
/// <param name="rating"></param>
/// <param name="username"></param>
/// <returns></returns>
        public RestaurantReviews(int id, string title, string body, int rating, string username) : this()
        {
            this.Id = id;
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
            this.Username = username;
        }
        /// <summary>
        /// Can return this also from the RevewJoin entity to get avgRating from a method in of UI handling the calculation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="rating"></param>
        /// <param name="username"></param>
        /// <param name="restaurantName"></param>
        /// <param name="avgRating"></param>
        /// <returns></returns>
        public RestaurantReviews(int id, string title, string body, int rating, string username, string restaurantName, decimal avgRating) : this()
        {
            this.Id = id;
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
            this.Username = username;
            this.RestaurantName = restaurantName;
            this.AvgRating = avgRating;
        }
        public string? Username {get; set;}

        public string? RestaurantName {get; set;}

        public decimal AvgRating {get; set; }
    }
/// <summary>
/// This is what the user inputs and is sent through BL/DL/Entities to store in the database.
/// </summary>
    public class CreateReview : Review
    {
         public CreateReview() {}
        public CreateReview(int id, string title, string body, int rating, int userId, int restaurantId) : this()
        {
            this.Id = id;
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
            this.UserId = userId;
            this.RestaurantId = restaurantId;
        }

        public int UserId {get; set;}

        public int RestaurantId {get; set;}
    }
}