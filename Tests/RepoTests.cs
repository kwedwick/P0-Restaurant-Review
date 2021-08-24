using Xunit;
using Entity = DL.Entities;
using Models;
using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tests
{
    public class RepoTests
    {

        private readonly DbContextOptions<Entity.restaurantreviewerContext> options;

        public RepoTests()
        {
            options = new DbContextOptionsBuilder<Entity.restaurantreviewerContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }

        [Fact]
        public void GetAllUsersShouldGetAllUsers()
        {
            //Given
            using (var context = new Entity.restaurantreviewerContext(options))
            {
                IUsersRepo _repo = new UsersRepo(context);
                //When
                var users = _repo.GetAllMembers();
                //Then
                Assert.Equal(4, users.Count);
            }
        }

        [Fact]
        public void GetAllRestuarantsShouldGetAllRestaurants()
        {
            //Given
            using (var context = new Entity.restaurantreviewerContext(options))
            {
                IRestaurantsRepo _repo = new RestaurantsRepo(context);
                //When
                var restaurants = _repo.GetAllRestaurants();
                //Then
                Assert.Equal(4, restaurants.Count);
            }
        }

        [Fact]
        public void GetAllReviewsShouldGetAllReviews()
        {
            //Given
            using (var context = new Entity.restaurantreviewerContext(options))
            {
                IReviewsRepo _repo = new ReviewsRepo(context);
                //When
                var reviews = _repo.GetAllReviews();
                //Then
                Assert.Equal(4, reviews.Count);
            }
        }

        [Fact]
        public void CreateUserShouldCreateAUser()
        {

            //Arrange
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IUsersRepo _repo = new UsersRepo(arrangeContext);

                //Act
                _repo.CreateUser(
                    new Models.Member
                    {
                        Id = 5,
                        FirstName = "Bob",
                        LastName = "Smith",
                        Email = "bsmith@gmail.com",
                        Username = "bsmith",
                        Password = "password1234",
                        IsAdmin = 0
                    }
                );
            }

            using (var assertContext = new Entity.restaurantreviewerContext(options))
            {

                Entity.User? user = assertContext.Users.FirstOrDefault(user => user.Id == 5);

                Assert.NotNull(user);
                Assert.Equal("Bob", user?.FirstName);
            }
        }

        [Fact]
        public void CreateRestaurantShouldCreateARestaurant()
        {

            //Arrange
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IRestaurantsRepo _repo = new RestaurantsRepo(arrangeContext);

                //Act
                _repo.CreateRestaurant(
                    new Models.Restaurants
                    {
                        Id = 5,
                        Name = "Bob's Burgers",
                        Location = "555 Hollywood Ave, Hollywood, CA",
                        ZipCode = 50670
                    }
                );
            }

            using (var assertContext = new Entity.restaurantreviewerContext(options))
            {

                Entity.Restaurant? restaurant = assertContext.Restaurants.FirstOrDefault(restaurant => restaurant.Id == 5);

                Assert.NotNull(restaurant);
                Assert.Equal("Bob's Burgers", restaurant?.Name);
            }
        }

        [Fact]
        public void CreateReviewShouldCreateAReview()
        {

            //Arrange
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IReviewsRepo _repo = new ReviewsRepo(arrangeContext);

                //Act
                _repo.CreateReview(
                    new Models.CreateReview
                    {
                        Id = 5,
                        TimeCreated = DateTime.Now,
                        Title = "Just okay",
                        Body = "This restaurant was okay but the staff were not as friendly. Karen was extremely rude. Food was good.",
                        Rating = 3,
                        UserId = 3,
                        RestaurantId = 2
                    }
                );
            }

            using (var assertContext = new Entity.restaurantreviewerContext(options))
            {

                Entity.Review review = assertContext.Reviews.FirstOrDefault(review => review.Id == 5);

                Entity.ReviewJoin reviewJoin = assertContext.ReviewJoins.FirstOrDefault(reviewJoin => reviewJoin.Id == 5);

                Assert.NotNull(review);
                Assert.Equal("Just okay", review.Title);

                Assert.NotNull(reviewJoin);
                Assert.Equal(3, reviewJoin.UserId);
            }
        }

        [Fact]
        public void GetRestaurantByNameShouldGetRestaurantByName()
        {

            //Arrange
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                //Given
                using (var context = new Entity.restaurantreviewerContext(options))
                {
                    IRestaurantsRepo _repo = new RestaurantsRepo(context);
                    //When
                    var restaurants = _repo.GetRestaurantByName("Culvers");
                    //Then
                    Assert.Equal("Culvers", restaurants.Name);
                    Assert.Equal(1, restaurants.Id);
                }
            }

        }

        [Fact]
        public void LoginShouldReturnUser()
        {
            //Given
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IUsersRepo _repo = new UsersRepo(arrangeContext);

                //Act
                var loggedInUser =
                _repo.GetUserLogin(
                    new Models.Member
                    {
                        Username = "kwedwick",
                        Password = "password1234",
                    }
                );

                Assert.NotNull(loggedInUser);
                Assert.Equal("kwedwick@gmail.com", loggedInUser.Email);
                Assert.Equal(1, loggedInUser.Id);
            }
        }

        [Fact]
        public void LoginShouldReturnEmptyUser()
        {
            //Given
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IUsersRepo _repo = new UsersRepo(arrangeContext);

                //Act
                var loggedInUser =
                _repo.GetUserLogin(
                    new Models.Member
                    {
                        Username = "pShermin",
                        Password = "42wallabyway",
                    }
                );

                Assert.NotNull(loggedInUser);
                Assert.Equal(null, loggedInUser.Email);
                Assert.Equal(0, loggedInUser.Id);
            }
        }

        [Fact]
        public void GetReviewsByRestaurantIDShouldReturnAllReviewsForThatRestaurant()
        {
            //Given
            using (var arrangeContext = new Entity.restaurantreviewerContext(options))
            {
                IReviewsRepo _repo = new ReviewsRepo(arrangeContext);

                //Act
                List<Models.RestaurantReviews> newReviews =
                _repo.GetReviewsbyRestaurantId(3);

                Assert.NotNull(newReviews);
                Assert.Equal(1, newReviews.Count);
            }
        }

        private void Seed()
        {
            using (var context = new Entity.restaurantreviewerContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Users.AddRange(
                    new Entity.User
                    {
                        Id = 1,
                        FirstName = "Keegan",
                        LastName = "Wedwick",
                        Email = "kwedwick@gmail.com",
                        Username = "kwedwick",
                        Password = "password1234",
                        IsAdmin = 1
                    }
                    ,
                    new Entity.User
                    {
                        Id = 2,
                        FirstName = "Brandon",
                        LastName = "Wedwick",
                        Email = "bwedwick@yahoo.com",
                        Username = "bwedwick",
                        Password = "password1234",
                        IsAdmin = 0
                    },
                    new Entity.User
                    {
                        Id = 3,
                        FirstName = "Linsi",
                        LastName = "Hagen",
                        Email = "lhagen@outlook.com",
                        Username = "lhagen",
                        Password = "password1234",
                        IsAdmin = 0
                    },
                    new Entity.User
                    {
                        Id = 4,
                        FirstName = "Troy",
                        LastName = "Neumann",
                        Email = "tneumann@msn.com",
                        Username = "tneumann",
                        Password = "password1234",
                        IsAdmin = 0
                    }
                );


                context.Restaurants.AddRange(
                    new Entity.Restaurant
                    {
                        Id = 1,
                        Name = "Culvers",
                        Location = "456 Deer Rd, Madison, WI",
                        Zipcode = 53562,
                    },
                    new Entity.Restaurant
                    {
                        Id = 2,
                        Name = "Subway",
                        Location = "555 Jackson Rd, Los Angeles, CA",
                        Zipcode = 90001,
                    },
                    new Entity.Restaurant
                    {
                        Id = 3,
                        Name = "Pizza Hut",
                        Location = "123 Sesame St, Baltimore City, MD",
                        Zipcode = 21201,
                    },
                    new Entity.Restaurant
                    {
                        Id = 4,
                        Name = "Panera",
                        Location = "789 Greenfield Ave, Chicago, IL",
                        Zipcode = 21201,
                    }
                );

                context.Reviews.AddRange(
                    new Entity.Review
                    {
                        Id = 1,
                        TimeCreated = DateTime.Now,
                        Title = "Just okay",
                        Body = "This restaurant was okay but the staff were not as friendly. Karen was extremely rude. Food was good.",
                        Rating = 3,
                    },
                    new Entity.Review
                    {
                        Id = 2,
                        TimeCreated = DateTime.Now,
                        Title = "OULD EAT AGAIN",
                        Body = "THE STAFF WERE SO NICE AND THE FOOD CAME OUT WARM! WOW! I''M SHOOKETH!",
                        Rating = 5,
                    },
                    new Entity.Review
                    {
                        Id = 3,
                        TimeCreated = DateTime.Now,
                        Title = "Pleasant Experience",
                        Body = "I called in a reservation and was told there would be a 3 day wait. I decided that was okay and the food was good. 1 star missing for the wait time.",
                        Rating = 4,
                    },
                    new Entity.Review
                    {
                        Id = 4,
                        TimeCreated = DateTime.Now,
                        Title = "Great'",
                        Body = "It''s what you expect.",
                        Rating = 4,
                    }
                );

                context.ReviewJoins.AddRange(
                    new Entity.ReviewJoin
                    {
                        Id = 1,
                        ReviewId = 1,
                        RestaurantId = 1,
                        UserId = 1,
                    },
                    new Entity.ReviewJoin
                    {
                        Id = 2,
                        ReviewId = 2,
                        RestaurantId = 2,
                        UserId = 2,
                    },
                    new Entity.ReviewJoin
                    {
                        Id = 3,
                        ReviewId = 3,
                        RestaurantId = 3,
                        UserId = 1,
                    },
                    new Entity.ReviewJoin
                    {
                        Id = 4,
                        ReviewId = 4,
                        RestaurantId = 4,
                        UserId = 4,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}