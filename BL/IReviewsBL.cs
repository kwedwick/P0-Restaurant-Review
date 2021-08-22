using Models;
using System.Collections.Generic;
namespace BL
{
    public interface IReviewsBL
    {
        List<Review> ViewAllReviews();

        Review AddReview(Review review);

        List<RestaurantReviews> GetReviewsbyRestaurantIdBL(int id);
    }
}