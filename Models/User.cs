namespace Models
{
    public interface IUser
    {

        string UserId { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string Role { get; set; }
        string Password { get; set; }
    }
    public class Member: IUser {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }

    public class Admin : IUser {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }

}


