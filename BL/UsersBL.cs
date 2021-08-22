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

        public Member AddUser(Member member)
        {
            return _repo.CreateUser(member);
        }

        public Member SearchUserById(int id)
        {
            return _repo.GetUserById(id);
        }

        public Member CheckUserLogin(Member member)
        {
            return _repo.GetUserLogin(member);
        }
    }
}