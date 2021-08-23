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

        public Models.Review CreateReview(Models.Review review)
        {
            _context.Reviews.Add(
                new Entities.Review
                {
                    Title = review.Title,
                    Body = review.Body,
                    Rating = review.Rating
                }
            );
            _context.SaveChanges();
            return review;
        }

        public List<Models.RestaurantReviews> GetReviewsbyRestaurantId(int id)
        {
            /// <summary>
            /// First filter ReviewJoins by Restaurant, then join in Review data, pass info to second join and then add in username.
            /// Need to change UserId to string so we can push in the user's username as string
            /// </summary>
            /// <returns>restaurantReviews</returns>
            List<Models.RestaurantReviews> restuarantReviews = _context.ReviewJoins
            .Where(reviewJoin => reviewJoin.RestaurantId == id)
            .Join(
                _context.Reviews,
                reviewJoin => reviewJoin.ReviewId,
                review => review.Id,
                (reviewJoin, review) => new Models.RestaurantReviews
                {
                    Id = review.Id,
                    Title = review.Title,
                    Body = review.Body,
                    Rating = review.Rating,
                    Username = reviewJoin.UserId.ToString()
                }
            )
            .Join(
                _context.Users,
                reviewJoin => reviewJoin.Id,
                userJoin => userJoin.Id,
                (reviewJoin, userJoin) => new Models.RestaurantReviews
                {
                    Id = reviewJoin.Id,
                    Title = reviewJoin.Title,
                    Body = reviewJoin.Body,
                    Rating = reviewJoin.Rating,
                    Username = userJoin.Username
                }
            )
            .ToList();
            
            return restuarantReviews;

        }
    }
}