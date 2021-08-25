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

        public Models.Restaurants GetRestaurantByName(string name)
        {
            Entities.Restaurant foundRestaurant = _context.Restaurants.FirstOrDefault(restaurant => restaurant.Name == name);

            if (foundRestaurant != null)
            {
                return new Models.Restaurants(foundRestaurant.Id, foundRestaurant.Name, foundRestaurant.Location, (int)foundRestaurant.Zipcode);
            }
            return new Models.Restaurants();
        }

        public Models.Restaurants CreateRestaurant(Models.Restaurants restaurant)
        {
            var newEntity = new Entities.Restaurant
                {
                    Name = restaurant.Name,
                    Location = restaurant.Location,
                    Zipcode = restaurant.ZipCode
                };
            _context.Restaurants.Add(newEntity);
            _context.SaveChanges();
            restaurant.Id = newEntity.Id;
            return restaurant;
        }



    }
}