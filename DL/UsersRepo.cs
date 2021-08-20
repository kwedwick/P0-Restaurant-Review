using Models;
using System.Collections.Generic;

namespace DL
{
    public class UsersRepo : IUsersRepo
    {
        public List<Member> GetAllMembers()
        {
            return new List<Member>();
        }
    }
}