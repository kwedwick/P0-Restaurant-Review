using Models;
using System.Collections.Generic;
namespace BL
{
    public interface IReviewsBL
    {
        List<Review> ViewAllReviews();

        CreateReview AddReview(CreateReview review);

        List<RestaurantReviews> GetReviewsbyRestaurantIdBL(int id);
    }
}