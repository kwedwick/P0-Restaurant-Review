using Models;
namespace UI
{


    public class Session
    {
        /// <summary>
        /// This is how we get the session to store a logged in users info
        /// It initializes as a null CurrentUser = not logged in
        /// </summary>
        public Member? CurrentUser { get; set; }
    }
}