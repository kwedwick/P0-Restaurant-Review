namespace Models
{
    /// <summary>
    /// This Model is how we link Restaurants, Users, and Reviews together
    /// </summary>
    public class ReviewJoin
    {
        public ReviewJoin() { }

        /// <summary>
        /// this is the setup for the ReviewJoin Table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reviewId"></param>
        /// <param name="restaurantId"></param>
        /// <param name="userId"></param>
        public ReviewJoin(int id, int reviewId, int restaurantId, int userId)
        {
            this.Id = id;
            this.ReviewId = reviewId;
            this.RestaurantId = restaurantId;
            this.UserId = userId;
        }

        /// <summary>
        /// This accepts users input, sends, and receives because UI-BL-DL-Entitiy
        /// It's how we can double join
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="rating"></param>
        /// <param name="username"></param>
        /// <param name="restaurantName"></param>
        /// <returns></returns>
        public ReviewJoin(int id, string title, string body, int rating, string username, string restaurantName) : this()
        {
            this.Id =id;
            this.Title = title;
            this.Body = body;
            this.Rating = rating;
            this.Username = username;
            this.RestaurantName = restaurantName;
        }

        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int RestaurantId { get; set; }

        public int UserId { get; set; }
        public System.DateTime TimeCreated { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int? Rating { get; set; }

        public string? Username {get; set;}

        public string? RestaurantName {get; set;}
    }
}