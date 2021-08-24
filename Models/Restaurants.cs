namespace Models
{
    /// <summary>
    /// Restaurant contiains the location, array of ratings submitted by user, and the reviews
    /// </summary>
    public class Restaurants
    {
        public Restaurants() { }

        public Restaurants(int id, string name, string location, int zipCode)
        {
            this.Id = id;
            this.Name = name;
            this.Location = location;
            this.ZipCode = zipCode;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        //public double AvgRating => Ratings.Average();
        public int ZipCode { get; set; }
    }
}