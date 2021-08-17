using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;
namespace Data
{
    public class RestaurantRepo
    {
        public static async Task<Restaurant> GetAllRestaurants(int Id){
            return new Restaurant() {
                Name = "Pizza Hut",
                Id = Id
            };
        }
        public static async Task<Restaurant>FindRestaurantById(int Id){
            return new Restaurant() {
                Name = "Subway",
                Id = Id
            };
        }

        public static async Task<Restaurant> CreateNewRestaurant(int Id){
            return new Restaurant() {
                Name = "Culvers",
                Id = Id
            };
        }

        public static async Task<Restaurant> UpdateRestaurant(int Id){
            return new Restaurant() {
                Name = "McDonalds",
                Id = Id
            };
        }

        public static async Task<Restaurant> DeleteRestaurant(int Id){
            return new Restaurant() {
                Name = "Firehouse Subs",
                Id = Id
            };
        }
    }
}