using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;
namespace Data
{
    public class UserRepo
    {
        public static async Task<Member> GetAllUsers(int Id){
            return new Member() {
                Email = "kwedwick@gmail.com",
                Id = Id
            };
        }
        public static async Task<Member> FindUserById(int Id){
            return new Member() {
                Email = "kwedwick@gmail.com",
                Id = Id
            };
        }

        public static async Task<Member> CreateNewUser(int Id){
            return new Member() {
                Email = "kwedwick@gmail.com",
                Id = Id
            };
        }

        public static async Task<Member> UpdateUser(int Id){
            return new Member() {
                Email = "kwedwick@gmail.com",
                Id = Id
            };
        }

        public static async Task<Member> DeleteUser(int Id){
            return new Member() {
                Email = "kwedwick@gmail.com",
                Id = Id
            };
        }
    }
}