using Models;
using DL;
using System.Collections.Generic;

namespace BL
{
    public class ReviewsBL : IReviewsBL
    {

        private IReviewsRepo _repo;

        public ReviewsBL(IReviewsRepo repo)
        {
            _repo = repo;
        }
        public List<Review> ViewAllReviews()
        {
            return _repo.GetAllReviews();
        }

        public CreateReview AddReview(CreateReview review)
        {
            return _repo.CreateReview(review);
        }

        public List<RestaurantReviews> GetReviewsbyRestaurantIdBL(int id)
        {
            return _repo.GetReviewsbyRestaurantId(id);
        }

        public List<RestaurantReviews> SeeMyReviews(int id)
        {
            return _repo.GetMyReviews(id);
        }
    }
}