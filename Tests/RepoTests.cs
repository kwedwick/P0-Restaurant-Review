using Xunit;
using Entity = DL.Entities;
using Models;
using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace Tests
{
    public class RepoTests
    {

        private readonly DbContextOptions<Entity.restaurantreviewerContext> options;

        public RepoTests() {
            options = new DbContextOptionsBuilder<Entity.restaurantreviewerContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
        [Fact]
        public void Test1()
        {

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