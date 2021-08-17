namespace Models
{
    /// <summary>
    /// interface to contract the classes
    /// </summary>
    public interface IUser
    {

        int Id { get; set; }

        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        bool IsAdmin { get; set; }
        string Password { get; set; }
    }
    public class Member : IUser {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
    }

    // public class Admin : IUser {

    //     public int Id { get; set; }
    //     public string FirstName { get; set; }
    //     public string LastName { get; set; }
    //     public string Username { get; set; }
    //     public string Email { get; set; }
    //     public bool IsAdmin { get; set; }
    //     public string Password { get; set; }
    // }

}


