using Models;
using System.Collections.Generic;
using System;
using DL.Entities;
using System.Linq;

namespace DL
{
    public class RestaurantsRepo : IRestaurantsRepo
    {
        private restaurantreviewerContext _context;

        public RestaurantsRepo(restaurantreviewerContext context)
        {
            _context = context;
        }

        public List<Models.Restaurants> GetAllRestaurants()
        {
            return _context.Restaurants.Select(
                restaurant => new Models.Restaurants(restaurant.Id, restaurant.Name, restaurant.Location, (int)restaurant.Zipcode)
            ).ToList();
        }
    }


    
}