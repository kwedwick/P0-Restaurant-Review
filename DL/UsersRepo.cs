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

        public Models.Member CreateUser(Models.Member member)
        {
            var newEntity = new Entities.User{
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Username = member.Username,
                    Email = member.Email,
                    Password = member.Password
                };
            _context.Users.Add(newEntity);
            _context.SaveChanges();
            member.Id = newEntity.Id;
            return member;
        }

        public Models.Member GetUserById(int id)
        {
            Entities.User foundUser = _context.Users.FirstOrDefault(
                user => user.Id == id
            );

            if (foundUser != null)
            {
                return new Models.Member(foundUser.Id, foundUser.FirstName, foundUser.LastName, foundUser.Username, foundUser.Email, foundUser.IsAdmin);
            }
            return new Models.Member();
        }

        public Models.Member GetUserLogin(Member member)
        {
            Entities.User foundUser = _context.Users.FirstOrDefault(user => user.Username == member.Username && user.Password == member.Password);

            if (foundUser != null)
            {
                return new Models.Member(foundUser.Id, foundUser.FirstName, foundUser.LastName, foundUser.Username, foundUser.Email, foundUser.IsAdmin);
            }
            return new Models.Member();
        }
    }
}