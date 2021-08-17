namespace Models
{
    /// <summary>
    /// Restaurant contiains the location, array of ratings submitted by user, and the reviews
    /// </summary>
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name {get; set; }
        public string Location { get; set; }
        //public double AvgRating => Ratings.Average();
        public int ZipCode { get; set; }
    }
}