using Models;
using DL;
using System.Collections.Generic;

namespace BL
{
    public class RestaurantsBL : IRestaurantsBL
    {
        private IRestaurantsRepo _repo;

        public RestaurantsBL(IRestaurantsRepo repo)
        {
            _repo = repo;
        }

        public List<Restaurants> ViewAllRestaurants()
        {
            return _repo.GetAllRestaurants();
        }

        public Restaurants SearchRestaurantByName(string name)
        {
            return _repo.GetRestaurantByName(name);
        }

        public Restaurants AddRestaurant(Restaurants restaurant)
        {
            return _repo.CreateRestaurant(restaurant);
        }
    }
}