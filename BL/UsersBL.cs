using Models;
using DL;
using System.Collections.Generic;

namespace BL
{
    public class UsersBL : IUsersBL
    {
        private IUsersRepo _repo;

        public UsersBL(IUsersRepo repo)
        {
            _repo = repo;
        }
        public List<Member> ViewAllUsers()
        {
            return _repo.GetAllMembers();
        }
    }
}