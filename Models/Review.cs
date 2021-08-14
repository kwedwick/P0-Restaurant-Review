namespace Models
{
    /// <summary>
    /// Users create a review and it's stored here using this class
    /// </summary>
    public class Review
    {
        public int ReviewId { get; set; }

        System.DateTime Created { get; set; }
        public string Title { get; set; }
        public string Body {get; set; }
        public string Username { get; set; }
        public int UserId {get; set; }
    }
}