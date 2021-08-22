using Models;
using System.Collections.Generic;

namespace DL
{   
    public interface IUsersRepo
    {
        List<Member> GetAllMembers();

        Member CreateUser(Member member);

        Member GetUserById(int id);

        Member GetUserLogin(Member member);
    }
}

