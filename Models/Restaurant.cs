namespace Models
{
    /// <summary>
    /// Restaurant contiains the location, array of ratings submitted by user, and the reviews
    /// </summary>
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string RestaurantName {get; set; }
        public string Location { get; set; }
        public decimal[] Rating { get; set; }
        public int ZipCode { get; set; }
        public string[] Reviews {get; set; }

    }
}