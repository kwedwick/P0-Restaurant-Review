namespace Models
{
    public class Reviews
    {
        public Reviews() { }
        public Reviews(int id, int reviewId, int restaurantId, int userId)
        {
            this.Id = id;
            this.ReviewId = reviewId;
            this.RestaurantId = restaurantId;
            this.UserId = userId;
        }
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int RestaurantId { get; set; }

        public int UserId { get; set; }
    }
}