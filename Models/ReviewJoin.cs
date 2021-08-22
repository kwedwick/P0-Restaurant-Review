namespace Models
{
    public class ReviewJoin
    {
        public ReviewJoin() { }
        public ReviewJoin(int id, int reviewId, int restaurantId, int userId)
        {
            this.Id = id;
            this.ReviewId = reviewId;
            this.RestaurantId = restaurantId;
            this.UserId = userId;
        }
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
        public string Title { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }

        public string Username {get; set;}

        public string RestaurantName {get; set;}
    }
}