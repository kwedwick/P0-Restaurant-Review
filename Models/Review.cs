namespace Models
{
    /// <summary>
    /// Users create a review and it's stored here using this class
    /// </summary>
    public class Review
    {
        public int Id { get; set; }

        System.DateTime TimeCreated { get; set; }
        public string Title { get; set; }
        public string Body {get; set; }
        public int Rating { get; set; }
        public int UserId {get; set; }
    }
}