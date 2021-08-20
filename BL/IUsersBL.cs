using System.Collections.Generic;
using Models;

namespace BL
{
    public interface IUsersBL
    {
        List<Member> ViewAllUsers();
    }
}