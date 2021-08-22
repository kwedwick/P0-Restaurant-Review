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
                    Username = userJoin.Username
                }
            )
            .ToList();
            
            // List<Models.RestaurantReviews> restuarantReviews = _context.ReviewJoins.Join(
            //     _context.Reviews,
            //     reviewJoin => reviewJoin.ReviewId,
            //     review => review.Id,
            //     (reviewJoin, review) => new Models.RestaurantReviews
            //     {
            //         Id = review.Id,
            //         Title = review.Title,
            //         Body = review.Body,
            //         Rating = review.Rating
            //     }
            // ).Join(
            //     _context.Users,
            //     reviewJoin2 => 
            //     userJoin => userJoin.Id,
            //     (ReviewJoin, Review) => new Models.RestaurantReviews
            //     {
            //         Username = 
            //     }
            // ).
            // ToList();

            return restuarantReviews;

        }
    }
}