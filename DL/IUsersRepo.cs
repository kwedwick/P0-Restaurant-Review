using Models;
using System.Collections.Generic;

namespace DL
{   
    public interface IUsersRepo
    {
        List<Member> GetAllMembers();
    }
}

