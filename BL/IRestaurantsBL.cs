using System.Collections.Generic;
using Models;

namespace BL
{
    public interface IRestaurantsBL
    {
        List<Restaurants> ViewAllRestaurants();

        Restaurants SearchRestaurantByName(string name);
    }
}