using System.Collections.Generic;
using Models;

namespace BL
{
    public interface IUsersBL
    {
        List<Member> ViewAllUsers();

        Member AddUser(Member member);

        Member SearchUserById(int id);

        Member CheckUserLogin(Member member);
    }
}