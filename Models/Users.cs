namespace Models
{
    /// <summary>
    /// interface to contract the classes
    /// </summary>
    public interface IUsers
    {

        int Id { get; set; }

        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        bool IsAdmin { get; set; }
        string Password { get; set; }
    }
    public class Member : IUsers
    {

        public Member() { }
        public Member(string firstName, string lastName, string username, string email, string password, bool isAdmin)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.IsAdmin = isAdmin;
        }

        public Member(int id) : this()
        {
            this.Id = id;
        }

        public Member(bool isAdmin) : this()
        {
            this.IsAdmin = isAdmin;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
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


