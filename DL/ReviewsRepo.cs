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

        public Models.CreateReview CreateReview(Models.CreateReview review)
        {
            var newEntity = new Entities.Review
            {
                Title = review.Title,
                Body = review.Body,
                Rating = review.Rating
            };
            _context.Reviews.Add(newEntity);
            _context.SaveChanges();
            review.Id = newEntity.Id;

            _context.ReviewJoins.Add(
                new Entities.ReviewJoin
                {
                    UserId = review.UserId,
                    RestaurantId = review.RestaurantId,
                    ReviewId = review.Id
                }
            );
            _context.SaveChanges();

            return review;
        }
        /// <summary>
        /// First filter ReviewJoins by Restaurant, then join in Review data pass info to second join and then add in username. Need to change. UserId to string so we can push in the user's username as string
        /// </summary>
        /// <param name="id"></param>
        /// <returns>restuarantReviews</returns>
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
                reviewJoin => reviewJoin.Username,
                userJoin => userJoin.Id.ToString(),
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

            if (restuarantReviews != null)
            {
                return restuarantReviews;
            }
            return new List<Models.RestaurantReviews>();

        }
        /// <summary>
        /// Gets logged in users reviews
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Models.RestaurantReviews> GetMyReviews(int id)
        {
            List<Models.RestaurantReviews> restuarantReviews = _context.ReviewJoins
           .Where(userReviews => userReviews.UserId == id)
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
                   RestaurantName = reviewJoin.RestaurantId.ToString()
               }
           )
           .Join(
               _context.Restaurants,
               reviewJoin => reviewJoin.RestaurantName,
               restaurantJoin => restaurantJoin.Id.ToString(),
               (reviewJoin, restaurantJoin) => new Models.RestaurantReviews
               {
                   Id = reviewJoin.Id,
                   Title = reviewJoin.Title,
                   Body = reviewJoin.Body,
                   Rating = reviewJoin.Rating,
                   RestaurantName = restaurantJoin.Name
               }
           )
           .ToList();
           
            if (restuarantReviews != null)
            {
                return restuarantReviews;
            }
            return new List<Models.RestaurantReviews>();
        }
    }
}