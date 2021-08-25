using Models;
using System.Collections.Generic;
using DL.Entities;
using System.Linq;
using System;

namespace DL
{
    /// <summary>
    /// Handles all User database requests
    /// </summary>
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

        /// <summary>
        /// Gets all members in the database and returns a list
        /// </summary>
        /// <returns>List of members</returns>
        public List<Models.Member> GetAllMembers()
        {
            //Console.WriteLine("You're in UsersRepo");
            return _context.Users.Select(
                user => new Models.Member(user.Id, user.FirstName, user.LastName, user.Username, user.Email, user.IsAdmin)
            ).ToList();
        }
/// <summary>
/// Inserts Member data
/// </summary>
/// <param name="member"></param>
/// <returns>user input and SQL created ID</returns>
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
/// <summary>
/// Get's member object by ID
/// </summary>
/// <param name="id"></param>
/// <returns>Single Member data</returns>
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
/// <summary>
/// Checks if username and password are matching a user and returns member
/// </summary>
/// <param name="member"></param>
/// <returns>Models.Member user</returns>
        public Models.Member GetUserLogin(Member member)
        {
            Entities.User foundUser = _context.Users.FirstOrDefault(user => user.Username == member.Username && user.Password == member.Password);

            if (foundUser != null)
            {
                return new Models.Member(foundUser.Id, foundUser.FirstName, foundUser.LastName, foundUser.Username, foundUser.Email, foundUser.IsAdmin);
            }
            return new Models.Member();
        }

/// <summary>
/// Finds if email is in the database
/// </summary>
/// <param name="email"></param>
/// <returns>string found email</returns>
        public string CheckUniqueEmail(string email)
        {
            var foundUser = _context.Users.FirstOrDefault(
                user => user.Email == email
            );

            if (foundUser != null)
            {
                return email = foundUser.Email;
            }
            return email = "";

        }
/// <summary>
/// Finds if username is in the database
/// </summary>
/// <param name="username"></param>
/// <returns>string found username</returns>
        public string CheckUniqueUsername(string username)
        {
             Entities.User foundUser = _context.Users.FirstOrDefault(
                user => user.Username == username
            );

            if (foundUser != null)
            {
                return username = foundUser.Username;
            }
            return username = "";
        }
    }
}