using Models;
namespace UI
{

/// <summary>
/// This is how we get the session to store a logged in users info
/// It initializes as a null CurrentUser = not logged in
/// </summary>
    public class Session
    {
        /// <summary>
        /// Sets CurrentUser to nullable type
        /// </summary>
        public Member? CurrentUser { get; set; }
    }
}