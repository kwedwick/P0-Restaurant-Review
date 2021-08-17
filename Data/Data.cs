using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

namespace Data
{
    // public class FileRepoJson
    // {
    //     private static string path= "JsonX.json";
    //     public static async void AddCats(List<Cat> cats){
    //         using (FileStream stream= File.Create(path))
    //         {
    //             try
    //             {
    //                await JsonSerializer.SerializeAsync(stream,cats);
    //             }
    //             catch(DriveNotFoundException){
    //                 throw;
    //             }
    //             catch (System.Exception)
    //             {
                    
    //                 throw;
    //             }
                
    //             System.Console.WriteLine("X are stored in the file");
    //         }
    //     }
    //     public static async Task<List<X>> GetX(){
    //         using (FileStream stream=File.OpenRead(path))
    //         {
    //             var X= await JsonSerializer.DeserializeAsync<List<X>>(stream);
    //             return X;
    //         }
    //     }
    // }
    public class UserRepo
    {
        public static async Task<Member> FindUserById(int Id){
            return new Member() {
                Email = "kwedwick@gmail.com",
                UserId = Id
            };
        }


    }
}