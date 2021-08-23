namespace Models
{
    /// <summary>
    /// Users create a review and it's stored here using this class
    /// </summary>
    public class Review
    {
        public Review() { }

        public Review(int id, System.DateTime timeCreated, string title, string body, int rating)
        {
            this.Id = id;
            this.TimeCreated = timeCreated;
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
        }

        public Review(string title, string body, int rating) : this()
        {
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
        }

        public int Id { get; set; }

        public System.DateTime TimeCreated { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
    }

    public class RestaurantReviews : Review
    {
        public RestaurantReviews() {}

        public RestaurantReviews(int id, string title, string body, int rating, string username) : this()
        {
            this.Id = id;
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
            this.Username = username;
        }
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
        public string Username {get; set;}

        public string RestaurantName {get; set;}

        public decimal AvgRating {get; set; }
    }

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