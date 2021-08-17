namespace Models
{
    /// <summary>
    /// interface to contract the classes
    /// </summary>
    public interface IUser
    {

        int UserId { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        bool IsAdmin { get; set; }
        string Password { get; set; }
    }
    public class Member: IUser {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
    }

    // public class Admin : IUser {
    //     public string UserId { get; set; }
    //     public string Username { get; set; }
    //     public string Email { get; set; }
    //     public string Role { get; set; }
    //     public string Password { get; set; }
    // }

}


