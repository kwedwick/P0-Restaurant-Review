using UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DL.Entities;
using BL;
using DL;
using System.IO;
using System;

/// <summary>
/// This is how we are hiding our SMSS UserID and Password. 
/// </summary>
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


/// <summary>
///  We are getting the db and passing it through
/// </summary>
string connectionString = configuration.GetConnectionString("p0restreviewerdb");

/// <summary>
/// We are creating the options to the server that we are using SQL Server
/// </summary>

DbContextOptions<restaurantreviewerContext> options = new DbContextOptionsBuilder<restaurantreviewerContext>()
    .UseSqlServer(connectionString)
    .Options;

/// <summary>
/// context is our instance of the database
/// </summary>

var context = new restaurantreviewerContext(options);


IMenu menu = new MainMenu(new UsersBL(new UsersRepo(context)), new RestaurantsBL(new RestaurantsRepo(context)), new ReviewsBL(new ReviewsRepo(context)));
menu.Start();