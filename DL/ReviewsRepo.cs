using Models;
using System.Collections.Generic;
using System;
using DL.Entities;
using System.Linq;

namespace DL
{
    public class ReviewsRepo : IReviewsRepo
    {
        private restaurantreviewerContext _context;

        public ReviewsRepo(restaurantreviewerContext context)
        {
            _context = context;
        }

        public List<Models.Review> GetAllReviews()
        {
            return _context.Reviews.Select(
                review => new Models.Review(review.Id, review.TimeCreated, review.Title, review.Body, review.Rating)
                ).ToList();
        }
    }
}