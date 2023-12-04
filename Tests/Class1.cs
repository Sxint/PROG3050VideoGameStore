using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PROG3050VideoGameStore.Controllers;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tests.Class1
{
    [TestFixture]
    public class ControllerTests
    {
        private AppDbContext _appDbContext;
        private AppDbContext _mockDbContext;
        private ILogger<LoginRegisterController> _logger;

        [SetUp]
        public void Setup()
        {
           
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=DESKTOP-KAJBKVO\\SQLEXPRESS;Database=GamesDatabase;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true")
                .Options;

            _appDbContext = new AppDbContext(options);

            var mockOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockDbContext = new AppDbContext(mockOptions);

            _logger = new Mock<ILogger<LoginRegisterController>>().Object;
        }

        [TearDown]
        public void TearDown()
        {
            _mockDbContext.Database.EnsureDeleted();
        }

        [Test]
        public void Login_ValidCredentials_RedirectsToProfileIndex()
        {
            // Arrange
            var controller = new LoginRegisterController(_logger, _appDbContext);

            var validModel = new LoginVM
            {
                Username = "Deepak",
                Password = "Deepak@2"
            };

            // Act
            RedirectToActionResult result = controller.Login(validModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ProfileIndex", result.ActionName);
            Assert.AreEqual("LoginRegister", result.ControllerName);
        }

        [Test]
        public void Login_InvalidUser_ReturnsViewWithError()
        {
            // Arrange
            var controller = new LoginRegisterController(_logger, _appDbContext);

            // Invalid credentials
            var invalidModel = new LoginVM
            {
                Username = "InvalidUser",
                Password = "InvalidPassword"
            };

            // Act
            ViewResult result = controller.Login(invalidModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
            Assert.AreEqual("Profile doesnt exist", result.ViewData["Message"]);
        }

        [Test]
        public void ListAddresses_ReturnsCorrectViewAndModel()
        {
            // Arrange
            var controller = new LoginRegisterController(_logger, _mockDbContext);

            // Mock data
            var userProfileId = 1; 
            var userAddresses = new List<UserAddress>
    {
        new UserAddress
        {
            Id = 2,
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            StreetAddress = "123 Main St",
            City = "City1",
            Province = "Province1",
            PostalCode = "12345",
            DeliveryInstructions = "Leave at the door",
            StreetAddressShipping = "456 Oak St",
            CityShipping = "City2",
            ProvinceShipping = "Province2",
            PostalCodeShipping = "54321",
            SameAsShipping = false,
            UserProfileId = userProfileId
        },
        new UserAddress
        {
            Id = 3,
            FirstName = "Jane",
            LastName = "Smith",
            PhoneNumber = "9876543210",
            StreetAddress = "789 Elm St",
            City = "City3",
            Province = "Province3",
            PostalCode = "67890",
            DeliveryInstructions = "Call before delivery",
            StreetAddressShipping = "789 Maple St",
            CityShipping = "City4",
            ProvinceShipping = "Province4",
            PostalCodeShipping = "09876",
            SameAsShipping = true,
            UserProfileId = userProfileId
        },
     
    };

            _mockDbContext.UserAddresses.AddRange(userAddresses);
            _mockDbContext.SaveChanges();

            // Act
            IActionResult result = controller.ListAddresses(userProfileId);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.AreEqual("ListAddresses", viewResult.ViewName);

            var model = viewResult.Model as AddressListVM;
            Assert.IsNotNull(model);
            Assert.AreEqual(userProfileId, model.ProfileId);

            var addressesInModel = model.AllAddresses
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    a.PhoneNumber,
                    a.StreetAddress,
                    a.AptNumber,
                    a.City,
                    a.Province,
                    a.PostalCode,
                    a.DeliveryInstructions,
                    a.StreetAddressShipping,
                    a.AptNumberShipping,
                    a.CityShipping,
                    a.ProvinceShipping,
                    a.PostalCodeShipping,
                    a.SameAsShipping
                })
                .ToList();

            var addressesInDatabase = _mockDbContext.UserAddresses
                .Where(a => a.UserProfileId == userProfileId)
                .OrderBy(a => a.StreetAddress)
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    a.PhoneNumber,
                    a.StreetAddress,
                    a.AptNumber,
                    a.City,
                    a.Province,
                    a.PostalCode,
                    a.DeliveryInstructions,
                    a.StreetAddressShipping,
                    a.AptNumberShipping,
                    a.CityShipping,
                    a.ProvinceShipping,
                    a.PostalCodeShipping,
                    a.SameAsShipping
                })
                .ToList();

            CollectionAssert.AreEqual(addressesInDatabase, addressesInModel);
        }

        [Test]
        public void AddAddress_ValidModel_RedirectsToProfileIndex()
        {
            // Arrange
            var controller = new LoginRegisterController(_logger, _mockDbContext);

            // Mock data
            var userProfileId = 1; 
            var validModel = new AddAddressVM
            {
                ProfileId = userProfileId,
                newUserAddress = new UserAddress
                {
                  
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                    StreetAddress = "123 Main St",
                    City = "City1",
                    Province = "Province1",
                    PostalCode = "12345",
                    DeliveryInstructions = "Leave at the door",
                    StreetAddressShipping = "456 Oak St",
                    CityShipping = "City2",
                    ProvinceShipping = "Province2",
                    PostalCodeShipping = "54321",
                    SameAsShipping = false
                }
            };

            // Act
            IActionResult result = controller.AddAddress(validModel);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("ProfileIndex", redirectResult.ActionName);
            Assert.AreEqual("LoginRegister", redirectResult.ControllerName);

         
            var addedAddress = _mockDbContext.UserAddresses
                .SingleOrDefault(a => a.UserProfileId == userProfileId && a.FirstName == "John" && a.LastName == "Doe");

            Assert.IsNotNull(addedAddress);
        }


        [Test]
        public void AddGame_ValidModel_RedirectsToPanel()
        {
            // Arrange
            var controller = new AdminController(_mockDbContext);

            var validModel = new AddGameVM
            {
                ProfileId = 1, 
                Game = new Game
                {
                    Name = "Test Game",
                    Author = "Test Author",
                    Description = "Test Description",
                    Price = 19.99,
                    Year = 2022,
                    Category = "Test Category",
                    Platform = "Test Platform"
                }
            };

            var returnNumber = validModel.ProfileId;

            // Act
            IActionResult result = controller.AddGame(validModel);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Panel", redirectResult.ActionName);
            Assert.AreEqual(1, redirectResult.RouteValues["id"]);

           
            var addedGame = _mockDbContext.Games
                .SingleOrDefault(g => g.Name == "Test Game");

            Assert.IsNotNull(addedGame);
        }

        [Test]
        public void AddEvent_ValidModel_RedirectsToPanel()
        {
            // Arrange
            var controller = new AdminController( _mockDbContext);

            var validModel = new EventVM
            {
                ProfileId = 1, 
                Event = new Event
                {
                    Name = "Test Event",
                    DateOfEvent = DateTime.Now.AddDays(7), // Set the event date to be in the future
                    TimeOfEvent = TimeSpan.FromHours(18), // Set the event time
                    Description = "Test Event Description"
                }
            };

            // Act
            IActionResult result = controller.AddEvent(validModel);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Panel", redirectResult.ActionName);
            Assert.AreEqual(1, redirectResult.RouteValues["id"]);

           
            var addedEvent = _mockDbContext.Events
                .SingleOrDefault(e => e.Name == "Test Event");

            Assert.IsNotNull(addedEvent);
        }

        [Test]
        public void DeleteGame_RedirectsToList()
        {
            // Arrange
            var controller = new AdminController(_mockDbContext);

            // Mock data
            var game = new Game
            {
                Id = 2, 
                Name = "Test Game",
                Author = "Test Author",
                Description = "Test Description",
                Price = 19.99,
                Year = 2022,
                Category = "Test Category",
                Platform = "Test Platform"
            };

            _mockDbContext.Games.Add(game);
            _mockDbContext.SaveChanges();

            // Act
            IActionResult result = controller.DeleteGame(game.Id, profileId: 1);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("List", redirectResult.ActionName);
            Assert.AreEqual(1, redirectResult.RouteValues["id"]);

          
            var deletedGame = _mockDbContext.Games.Find(game.Id);

            Assert.IsNull(deletedGame);
        }

        [Test]
        public void DeleteEvent_RedirectsToEventList()
        {
            // Arrange
            var controller = new AdminController(_mockDbContext);

            // Mock data
            var eventModel = new Event
            {
                Id = 2, 
                Name = "Test Event",
                DateOfEvent = DateTime.Now.AddDays(7), // Set the event date to be in the future
                TimeOfEvent = TimeSpan.FromHours(18), // Set the event time
                Description = "Test Event Description"
            };

            _mockDbContext.Events.Add(eventModel);
            _mockDbContext.SaveChanges();

            // Act
            IActionResult result = controller.DeleteEvent(eventModel.Id, profileId: 1);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("EventList", redirectResult.ActionName);
            Assert.AreEqual(1, redirectResult.RouteValues["id"]);

           
            var deletedEvent = _mockDbContext.Events.Find(eventModel.Id);

            Assert.IsNull(deletedEvent);
        }

        [Test]
        public void AddRatings_NewRating_AddsRatingAndRedirectsToView()
        {
            // Arrange
            var controller = new GamesController(_mockDbContext);

            // Mock data
            var game = new Game
            {
                Id = 1,
                Name = "Test Game",
                Author = "Test Author",
                Description = "Test Description",
                Price = 19.99,
                Year = 2022,
                Category = "Test Category",
                Platform = "Test Platform"
            };

            var userProfile = new UserProfile
            {
                Id = 1,
                DisplayName = "TestUser",
                Email = "testuser@example.com",
                Password = "Password123",
            };

            var userProfileId = 1;

            _mockDbContext.Profiles.Add(userProfile);
            _mockDbContext.Games.Add(game);
            _mockDbContext.SaveChanges();

            var validModel = new RatingVM
            {
                GameId = game.Id,
                ProfileId = userProfileId,
                NewRating = new Rating
                {
                    RatingValue = 4,
                    UserProfileId = userProfileId,
                    GameId = game.Id
                }
            };

            try
            {
                // Act
                IActionResult result = controller.AddRatings(validModel);

                // Assert
                Assert.IsInstanceOf<ViewResult>(result);

                var viewResult = result as ViewResult;
                Assert.AreEqual("AddRatings", viewResult.ViewName);

                var model = viewResult.Model as RatingVM;
                Assert.IsNotNull(model);
                Assert.AreEqual(userProfileId, model.ProfileId);

                var addedRating = _mockDbContext.Rating
                    .SingleOrDefault(r => r.UserProfileId == userProfileId && r.GameId == game.Id);

                Assert.IsNotNull(addedRating);
                Assert.AreEqual(validModel.NewRating.RatingValue, addedRating.RatingValue);
            }
            finally
            {
                // Clean up
                _mockDbContext.Rating.RemoveRange(_mockDbContext.Rating);
                _mockDbContext.SaveChanges();
            }
        }


        [Test]
        public void AddRatings_InvalidModel_ReturnsViewWithError()
        {
            // Arrange
            var controller = new GamesController(_mockDbContext);

            // Mock data
            var game = new Game
            {
                Id = 1,
                Name = "Test Game",
                Author = "Test Author",
                Description = "Test Description",
                Price = 19.99,
                Year = 2022,
                Category = "Test Category",
                Platform = "Test Platform"
            };

            var userProfile = new UserProfile
            {
                Id = 1,
                DisplayName = "TestUser",
                Email = "testuser@example.com",
                Password = "Password123",
            };

            var userProfileId = 1;

            _mockDbContext.Profiles.Add(userProfile);
            _mockDbContext.Games.Add(game);
            _mockDbContext.SaveChanges();

            var invalidModel = new RatingVM
            {
                GameId = game.Id,
                ProfileId = userProfileId,
                NewRating = new Rating
                {
                    
                    RatingValue = 6,
                    UserProfileId = userProfileId,
                    GameId = game.Id
                }
            };

            try
            {
                // Act
                IActionResult result = controller.AddRatings(invalidModel);

                // Assert
                Assert.IsInstanceOf<ViewResult>(result);

                var viewResult = result as ViewResult;
                Assert.AreEqual("AddRatings", viewResult.ViewName);

                var model = viewResult.Model as RatingVM;
                Assert.IsNotNull(model);
                Assert.AreEqual(userProfileId, model.ProfileId);

                // Ensure that ViewData["Message"] is not null and is an empty string
               
                Assert.AreEqual(string.Empty, viewResult.ViewData["Message"]);

                // The following assertions are similar to the valid model test
                var addedOrUpdatedRating = _mockDbContext.Rating
                    .SingleOrDefault(r => r.UserProfileId == userProfileId && r.GameId == game.Id);

                Assert.IsNull(addedOrUpdatedRating); // Ensure that no rating is added or updated
            }
            finally
            {
                // Clean up
                _mockDbContext.Rating.RemoveRange(_mockDbContext.Rating);
                _mockDbContext.SaveChanges();
            }
        }


        [Test]
        public void AddReview_ValidReview_RedirectsToIndex()
        {
            // Arrange
            var controller = new GamesController(_mockDbContext);

            // Mock data
            var game = new Game
            {
                Id = 1,
                Name = "Test Game",
                Author = "Test Author",
                Description = "Test Description",
                Price = 19.99,
                Year = 2022,
                Category = "Test Category",
                Platform = "Test Platform"
            };

            var userProfile = new UserProfile
            {
                Id = 1,
                DisplayName = "TestUser",
                Email = "testuser@example.com",
                Password = "Password123",
            };

            var userProfileId = 1;

            _mockDbContext.Profiles.Add(userProfile);
            _mockDbContext.Games.Add(game);
            _mockDbContext.SaveChanges();

            var validModel = new ReviewVM
            {
                GameId = game.Id,
                ProfileId = userProfileId,
                NewReview = new Review
                {
                    ReviewText = "This is a valid review.",
                    UserProfileId = userProfileId,
                    GameId = game.Id,
                    IsReviewed = false,
                    ReviewBy = "TestReviewer"
                }
            };

            try
            {
                // Act
                IActionResult result = controller.AddReview(validModel);

                // Assert
                Assert.IsInstanceOf<RedirectToActionResult>(result);

                var redirectResult = result as RedirectToActionResult;
                Assert.IsNotNull(redirectResult);
                Assert.AreEqual("Index", redirectResult.ActionName);
                Assert.AreEqual("Games", redirectResult.ControllerName);
                Assert.AreEqual(userProfileId, redirectResult.RouteValues["id"]);
            }
            finally
            {
                // Clean up
                _mockDbContext.Review.RemoveRange(_mockDbContext.Review);
                _mockDbContext.SaveChanges();
            }
        }


        [Test]
        public void AddReview_InvalidReview_ReturnsViewWithError()
        {
            // Arrange
            var controller = new GamesController(_mockDbContext);

            // Mock data
            var game = new Game
            {
                Id = 1,
                Name = "Test Game",
                Author = "Test Author",
                Description = "Test Description",
                Price = 19.99,
                Year = 2022,
                Category = "Test Category",
                Platform = "Test Platform"
            };

            var userProfile = new UserProfile
            {
                Id = 1,
                DisplayName = "TestUser",
                Email = "testuser@example.com",
                Password = "Password123",
            };

            var userProfileId = 1;

            _mockDbContext.Profiles.Add(userProfile);
            _mockDbContext.Games.Add(game);
            _mockDbContext.SaveChanges();

            var invalidModel = new ReviewVM
            {
                GameId = game.Id,
                ProfileId = userProfileId,
                NewReview = new Review
                {
                    
                    ReviewText = "",
                    UserProfileId = userProfileId,
                    GameId = game.Id,
                    IsReviewed = false,
                    ReviewBy = "TestReviewer"
                }
            };

            try
            {
                // Act
                IActionResult result = controller.AddReview(invalidModel);

                // Assert
                Assert.IsInstanceOf<ViewResult>(result);

                var viewResult = result as ViewResult;
                Assert.AreEqual("AddReview", viewResult.ViewName);

                var model = viewResult.Model as ReviewVM;
                Assert.IsNotNull(model);
                Assert.AreEqual(userProfileId, model.ProfileId);
                Assert.AreEqual(game.Id, model.GameId);

                // Check ViewData
                Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewData["Message"]?.ToString()));
            }
            finally
            {
                // Clean up
                _mockDbContext.Review.RemoveRange(_mockDbContext.Review);
                _mockDbContext.SaveChanges();
            }
        }

        [Test]
        public void WishList_WithGames_ReturnsViewWithGames()
        {
            // Arrange
            var controller = new WishListController(_mockDbContext);

            // Mock data
            var userProfileId = 1;

            var game1 = new Game
            {
                Id = 1,
                Name = "Game 1",
                Author = "Author 1",
                Description = "Description 1",
                Price = 19.99,
                Year = 2022,
                Category = "Category 1",
                Platform = "Platform 1",
           
            };

            var game2 = new Game
            {
                Id = 2,
                Name = "Game 2",
                Author = "Author 2",
                Description = "Description 2",
                Price = 24.99,
                Year = 2021,
                Category = "Category 2",
                Platform = "Platform 2",
         
            };

            var wishList = new List<WishList>
    {
        new WishList { Id = 1, GameId = game1.Id, UserProfileId = userProfileId, Game = game1 },
        new WishList { Id = 2, GameId = game2.Id, UserProfileId = userProfileId, Game = game2 }
    };

            _mockDbContext.WishlistItems.AddRange(wishList);
            _mockDbContext.SaveChanges();

            try
            {
                // Act
                IActionResult result = controller.WishList(userProfileId);

                // Assert
                Assert.IsInstanceOf<ViewResult>(result);

                var viewResult = result as ViewResult;
                Assert.AreEqual("WishList", viewResult.ViewName);

                var model = viewResult.Model as AllGamesVM;
                Assert.IsNotNull(model);
                Assert.AreEqual(userProfileId, model.ProfileId);
                Assert.IsNotNull(model.AllGames);
                Assert.AreEqual(2, model.AllGames.Count); // Assuming 2 games in the wishlist

               
            }
            finally
            {
                // Clean up
                _mockDbContext.WishlistItems.RemoveRange(_mockDbContext.WishlistItems);
                _mockDbContext.SaveChanges();
            }
        }

        [Test]
        public void WishList_WithoutGames_ReturnsViewWithoutGames()
        {
            // Arrange
            var controller = new WishListController(_mockDbContext);

            // Mock data
            var userProfileId = 1;

            // No games in the wishlist

            try
            {
                // Act
                IActionResult result = controller.WishList(userProfileId);

                // Assert
                Assert.IsInstanceOf<ViewResult>(result);

                var viewResult = result as ViewResult;
                Assert.AreEqual("WishList", viewResult.ViewName);

                var model = viewResult.Model as AllGamesVM;
                Assert.IsNotNull(model);
                Assert.AreEqual(userProfileId, model.ProfileId);
                Assert.IsNotNull(model.AllGames);
                Assert.AreEqual(0, model.AllGames.Count); // No games in the wishlist

            }
            finally
            {
                
            }
        }

        [Test]
        public void DownloadGamesDetails_ReturnsFileResult()
        {
            // Arrange
            
            var controller = new Reports(_mockDbContext);

            // Act
            IActionResult result = controller.DownloadGamesDetails();

            // Assert
            Assert.IsInstanceOf<FileResult>(result);

            var fileResult = result as FileResult;
            Assert.AreEqual("application/pdf", fileResult.ContentType);
            Assert.AreEqual("GamesDetails.pdf", fileResult.FileDownloadName);
           
        }

        [Test]
        public void DownloadEventsDetails_ReturnsFileResult()
        {
            // Arrange
            var controller = new Reports(_mockDbContext);

            // Act
            IActionResult result = controller.DownloadEventsDetails();

            // Assert
            Assert.IsInstanceOf<FileResult>(result);

            var fileResult = result as FileResult;
            Assert.AreEqual("application/pdf", fileResult.ContentType);
            Assert.AreEqual("EventsDetails.pdf", fileResult.FileDownloadName);
          
        }

        [Test]
        public void Register_UserNotRegistered_ReturnsViewWithMessage()
        {
            // Arrange
            var controller = new GamesController(_mockDbContext);

            // Mock data
            var userProfile = new UserProfile
            {
                Id = 100,
                DisplayName = "TestUser",
                Email = "testuser@example.com",
                Password = "Password123",
            };

            var eventItem = new Event
            {
                Id = 2,
                Name = "Event 2",
                DateOfEvent = DateTime.Now.AddDays(7), // Set the date to a future date
                TimeOfEvent = new TimeSpan(14, 0, 0), // Set the time
                Description = "Test Event Description"
            };

            _mockDbContext.Profiles.Add(userProfile);
            _mockDbContext.Events.Add(eventItem);
            _mockDbContext.SaveChanges();

            try
            {
                // Act
                IActionResult result = controller.Register(eventItem.Id, userProfile.Id);

                // Assert
                Assert.IsInstanceOf<ViewResult>(result);

                var viewResult = result as ViewResult;
                Assert.AreEqual("EventRegister", viewResult.ViewName);

                Assert.AreEqual($"You have been registered for the {eventItem.Name} Event", viewResult.ViewData["Message"]);
            }
            finally
            {
                // Clean up
                _mockDbContext.AllParticipations.RemoveRange(_mockDbContext.AllParticipations);
                _mockDbContext.SaveChanges();
            }
        }

        [Test]
        public void Register_UserAlreadyRegistered_ReturnsViewWithMessage()
        {
            // Arrange
            var controller = new GamesController(_mockDbContext);

            // Mock data
            var userProfile = new UserProfile
            {
                Id = 100,
                DisplayName = "TestUser",
                Email = "testuser@example.com",
                Password = "Password123",
            };

            var eventItem = new Event
            {
                Id = 2,
                Name = "Event 2",
                DateOfEvent = DateTime.Now.AddDays(7), // Set the date to a future date
                TimeOfEvent = new TimeSpan(14, 0, 0), // Set the time
                Description = "Test Event Description"
            };

            var existingParticipation = new EventParticipation
            {
                UserProfileId = userProfile.Id,
                EventId = eventItem.Id
            };

            _mockDbContext.Profiles.Add(userProfile);
            _mockDbContext.Events.Add(eventItem);
            _mockDbContext.AllParticipations.Add(existingParticipation);
            _mockDbContext.SaveChanges();

            try
            {
                // Act
                IActionResult result = controller.Register(eventItem.Id, userProfile.Id);

                // Assert
                Assert.IsInstanceOf<ViewResult>(result);

                var viewResult = result as ViewResult;
                Assert.AreEqual("EventRegister", viewResult.ViewName);

                Assert.AreEqual("You are already registered for this event", viewResult.ViewData["Message"]);
            }
            finally
            {
                // Clean up
                _mockDbContext.AllParticipations.RemoveRange(_mockDbContext.AllParticipations);
                _mockDbContext.SaveChanges();
            }
        }

        [Test]
        public void DownloadGame_ReturnsTextFile()
        {
            // Arrange
            var controller = new Orders(_mockDbContext);

            // Mock data
            var gameId = 1; // Replace with a valid game ID in your database
            var gameName = "Test Game";
            var gameAuthor = "Test Author";
            var gameDescription = "Test Description";
            var gamePrice = 19.99;
            var gameYear = 2022;
            var gameCategory = "Test Category";
            var gamePlatform = "Test Platform";

           
            _mockDbContext.Games.Add(new Game
            {
                Id = gameId,
                Name = gameName,
                Author = gameAuthor,
                Description = gameDescription,
                Price = gamePrice,
                Year = gameYear,
                Category = gameCategory,
                Platform = gamePlatform
               
            });
            _mockDbContext.SaveChanges();

            try
            {
                // Act
                var result = controller.DownloadGame(gameId);

                // Assert
                Assert.IsInstanceOf<FileResult>(result);

                var fileResult = result as FileResult;
                Assert.AreEqual("Game.txt", fileResult.FileDownloadName);
                Assert.AreEqual("text/plain", fileResult.ContentType);

               
            }
            finally
            {
                // Clean up
                _mockDbContext.Games.RemoveRange(_mockDbContext.Games);
                _mockDbContext.SaveChanges();
            }
        }

        [Test]
        public void PurchasedGames_ValidProfileId_ReturnsViewWithModel()
        {
            // Arrange

            var controller = new Orders(_mockDbContext);
            var game = new Game
            {
                Id = 1,
                Name = "Test Game",
                Author = "Test Author",
                Description = "Test Description",
                Price = 29.99,  
                Year = 2022,
                Category = "Action",
                Platform = "PC",
         
            };

            var orderItem = new OrderItem
            {
                GameId = game.Id,
                Order = new Order
                {
                    CardHolderName = "Deepak Nirmal",
                    CardNumber = "1234567890123456",
                    ExpiryDate = "12/25",
                    CVCNumber = "123",
                    OrderStatus = "Confirmed",

                  
                }
            };

            var userProfile = new UserProfile
            {
                Id = 1,
                DisplayName = "DeepakNirmal",
                Password = "Test@Password1",
                ActualName = "Deepak Nirmal",
                Gender = "Male",
                BirthDate = new DateTime(1990, 1, 1),
                PromotionEmail = true,
                Email = "Deepak.Nirmal@example.com",
                EmailValidate = true,
                IsEmployee = false,
                RememberMe = true,
                RepeatedInvalidCreds = 0,
               
            };

            var wishList = new WishList
            {
                Id = 1,
                GameId = game.Id,
                UserProfileId = userProfile.Id
            };

            var orderList = new List<Order>
        {
            new Order
            {
                Id = 1,
                CardHolderName = "Deepak Nirmal",
                CardNumber = "1234567890123456",
                ExpiryDate = "12/25",
                CVCNumber = "123",
                OrderStatus = "Confirmed",
                TotalCost = 29.99,  
                UserProfileId = userProfile.Id,
                OrderItems = new List<OrderItem> { orderItem }

            }
        };

            // Add data to the in-memory database
            _mockDbContext.AddRange(game, orderItem.Order, orderItem, userProfile, wishList);
            foreach (var order in orderList)
            {
                var existingOrder = _mockDbContext.Orders.Find(order.Id);
                if (existingOrder != null)
                {
                    _mockDbContext.Entry(existingOrder).State = EntityState.Detached;
                }

                _mockDbContext.Orders.Add(order);
            }
            _mockDbContext.SaveChanges();

            try
            {
                // Act
                var result = controller.PurchasedGames(userProfile.Id) as ViewResult;
                var model = result?.Model as ListVM;

                // Assert
                Assert.IsInstanceOf<ViewResult>(result);
                Assert.NotNull(model);
                Assert.AreEqual(userProfile.Id, model.ProfileId);
                Assert.AreEqual(1, model.AllGames.Count);
            }
            finally
            {
                // Clean up
                _mockDbContext.OrderItems.RemoveRange(_mockDbContext.OrderItems);
                _mockDbContext.Orders.RemoveRange(_mockDbContext.Orders);
                _mockDbContext.WishlistItems.RemoveRange(_mockDbContext.WishlistItems);
                _mockDbContext.Games.RemoveRange(_mockDbContext.Games);
                _mockDbContext.Profiles.RemoveRange(_mockDbContext.Profiles);

                _mockDbContext.SaveChanges();
            }


        }

      

        [Test]
        public void UpdateProfile_InvalidModel_ReturnsViewWithoutMessage()
        {
            // Arrange
            var controller = new LoginRegisterController(_logger,_mockDbContext);
            var userProfile = new UserProfile
            {
                Id = 1,
                DisplayName = "TestUser",
                Password = "Test@Password1",
                ActualName = "Deepak Nirmal",
                Gender = "Male",
                BirthDate = new DateTime(1990, 1, 1),
                PromotionEmail = true,
                Email = "deepak.nirmal@example.com",
                EmailValidate = true,
                IsEmployee = false,
                RememberMe = true,
                RepeatedInvalidCreds = 0,
                CurrentPrefId = 1,
                CurrentAddressId = 1,
                Preferences = new ProfilePreferences
                {
                    Id = 1,
                    FavCategory = "Action",
                    FavPlatform = "PC",
                    Language = "English"
                },
                Address = new UserAddress
                {
                    Id = 1,
                    FirstName = "Deepak",
                    LastName = "Nirmal",
                    PhoneNumber = "1234567890",
                    StreetAddress = "123 Main St",
                    AptNumber = "Apt 456",
                    City = "City",
                    Province = "Ontario",
                    PostalCode = "12345",
                    DeliveryInstructions = "Leave at the door",
                    StreetAddressShipping = "456 Shipping St",
                    AptNumberShipping = "Apt 789",
                    CityShipping = "Shipping City",
                    ProvinceShipping = "Ontario",
                    PostalCodeShipping = "54321",
                    SameAsShipping = false
                },
                RatingItem = new Rating
                {
                    Id = 1,
                    RatingValue = 5
                },
                ReviewItem = new Review
                {
                    Id = 1,
                    ReviewText = "Great game!",
                    IsReviewed = true,
                    ReviewBy = "Deepak Nirmal",
                }
            };
            controller.ModelState.AddModelError("DisplayName", "Display name is required"); 

            // Act
            var result = controller.UpdateProfile(userProfile) as ViewResult;
            var model = result?.Model as UserProfile;

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsEmpty(result?.ViewData["Message"].ToString());
            Assert.NotNull(model);
   
        }

    }
}
