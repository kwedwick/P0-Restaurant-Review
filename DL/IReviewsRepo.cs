using Models;
using System.Collections.Generic;

namespace DL
{
    public interface IReviewsRepo
    {
        List<Review> GetAllReviews();

        Review CreateReview(Review review);

        List<RestaurantReviews> GetReviewsbyRestaurantId(int id);
    }
}