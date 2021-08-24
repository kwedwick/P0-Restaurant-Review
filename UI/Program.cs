using UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DL.Entities;
using BL;
using DL;
using System.IO;
using System;

// This is how we are hiding our SMSS UserID and Password. 
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

//  We are getting the db and passing it through
string connectionString = configuration.GetConnectionString("p0restreviewerdb");

// We are creating the options to the server that we are using SQL Server
DbContextOptions<restaurantreviewerContext> options = new DbContextOptionsBuilder<restaurantreviewerContext>()
    .UseSqlServer(connectionString)
    .Options;

// context is our instance of the database
var context = new restaurantreviewerContext(options);
var session = new Session();


// This is how the different projects communicate with the db. We need to inject the context into DL layer, then BL, layer, and then the MainMenu
IMenu menu = new MainMenu(
    new UsersBL(new UsersRepo(context)),
    new RestaurantsBL(new RestaurantsRepo(context)),
    new ReviewsBL(new ReviewsRepo(context)),
    session);

// Calling the start function to start app
menu.Start();