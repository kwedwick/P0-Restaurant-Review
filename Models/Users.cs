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
        int IsAdmin { get; set; }
        string Password { get; set; }
    }
    public class Member : IUsers
    {

        public Member() { }
        public Member(int isAdmin) : this()
        {
            this.IsAdmin = isAdmin;
        }

        public Member(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public Member(string firstName, string lastName, string username, string email, int isAdmin)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.Email = email;
            this.IsAdmin = isAdmin;
        }
        public Member(int Id, string firstName, string lastName, string username, string email, int isAdmin)
        {
            this.Id = Id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.Email = email;
            this.IsAdmin = isAdmin;
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; }
    }

}


