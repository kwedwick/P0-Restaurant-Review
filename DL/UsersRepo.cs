using Models;
using System.Collections.Generic;
using DL.Entities;
using System.Linq;
using System;

namespace DL
{
    public class UsersRepo : IUsersRepo
    {

        /// <summary>
        /// referencing the Entities context
        /// </summary>
        private restaurantreviewerContext _context;

        /// <summary>
        /// injecting the context into the UsersRepo class
        /// </summary>
        /// <param name="context"></param>
        public UsersRepo(restaurantreviewerContext context)
        {
            _context = context;
        }

        
        public List<Models.Member> GetAllMembers()
        {
            //Console.WriteLine("You're in UsersRepo");
            return _context.Users.Select(
                user => new Models.Member(user.Id, user.FirstName, user.LastName, user.Username, user.Email, user.IsAdmin)
            ).ToList();
        }
    }
}