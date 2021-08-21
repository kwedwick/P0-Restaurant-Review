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
}