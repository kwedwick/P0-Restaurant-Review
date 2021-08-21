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
    }
}