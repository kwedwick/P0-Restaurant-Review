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

        public Review AddReview(Review review)
        {
            return _repo.CreateReview(review);
        }
    }
}