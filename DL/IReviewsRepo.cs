using Models;
using System.Collections.Generic;

namespace DL
{
    public interface IReviewsRepo
    {
        List<Review> GetAllReviews();

        CreateReview CreateReview(CreateReview review);

        List<RestaurantReviews> GetReviewsbyRestaurantId(int id);

        List<RestaurantReviews> GetMyReviews(int id);
    }
}