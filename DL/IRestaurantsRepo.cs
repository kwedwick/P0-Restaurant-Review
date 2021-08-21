using Models;
using System.Collections.Generic;
namespace DL
{
    public interface IRestaurantsRepo
    {
        List<Restaurants> GetAllRestaurants();
    }
}