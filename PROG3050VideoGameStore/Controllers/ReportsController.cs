using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;
using System.Dynamic;
using System.Globalization;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using iText.Layout;
using PROG3050VideoGameStore.ViewModels.Reports;

namespace PROG3050VideoGameStore.Controllers
{
    public class Reports : Controller
    {

        private string connectionString = "Server=DESKTOP-KAJBKVO\\SQLEXPRESS;Database=GamesDatabase;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        public Reports(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult ReportsPage(int id = 0)
        {
            ReportsVM model = new ReportsVM();
            model.ProfileId = id;
            return View("ReportsPage",model);
        }

        public ActionResult DownloadGamesDetails(int id=0)
        {
            string query = "SELECT Id, Name, Author, Description, Price, Year, Category, Platform FROM Games";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var records = new List<Game>();

                        while (reader.Read())
                        {
                            var record = new Game
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Author = reader.GetString(reader.GetOrdinal("Author")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Price = reader.GetDouble(reader.GetOrdinal("Price")),
                                Year = reader.GetInt32(reader.GetOrdinal("Year")),
                                Category = reader.GetString(reader.GetOrdinal("Category")),
                                Platform = reader.GetString(reader.GetOrdinal("Platform")),
                            };

                            records.Add(record);
                        }

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Create a PDF document
                            using (PdfWriter writer = new PdfWriter(memoryStream))
                            {
                                using (PdfDocument pdf = new PdfDocument(writer))
                                {
                                    Document document = new Document(pdf);

                                    // Add a title to the PDF
                                    document.Add(new Paragraph("Details of all the games in the database"));

                                    // Add game details to the PDF
                                    foreach (var record in records)
                                    {
                                        document.Add(new Paragraph($"Name: {record.Name}, Author: {record.Author}, Description: {record.Description}, " +
                                            $"Price: {record.Price}, Year: {record.Year}, Category: {record.Category}, Platform: {record.Platform}"));
                                    }
                                }
                            }

                            // Convert the PDF document to bytes
                            byte[] fileContents = memoryStream.ToArray();

                            // Provide the PDF file for download
                            return File(fileContents, "application/pdf", "GamesDetails.pdf");
                        }
                    }
                }
            }

            return RedirectToAction("ReportsPage", "Reports", new
            {
                id = id
            });
        }

        public ActionResult DownloadEventsDetails(int id = 0)
        {
            string query = "SELECT Id, Name, DateOfEvent, TimeOfEvent, Description FROM Events";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var records = new List<Event>();

                        while (reader.Read())
                        {
                            var record = new Event
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                DateOfEvent = reader.GetDateTime(reader.GetOrdinal("DateOfEvent")),
                                TimeOfEvent = reader.GetTimeSpan(reader.GetOrdinal("TimeOfEvent")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                            };

                            records.Add(record);
                        }

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Create a PDF document
                            using (PdfWriter writer = new PdfWriter(memoryStream))
                            {
                                using (PdfDocument pdf = new PdfDocument(writer))
                                {
                                    Document document = new Document(pdf);

                                    // Add a title to the PDF
                                    document.Add(new Paragraph("Details of all the events in the database"));

                                    // Add event details to the PDF
                                    foreach (var record in records)
                                    {
                                        document.Add(new Paragraph($"Name: {record.Name}, Date of Event: {record.DateOfEvent.ToShortDateString()}, " +
                                            $"Time of Event: {record.TimeOfEvent}, Description: {record.Description}"));
                                    }
                                }
                            }

                            // Convert the PDF document to bytes
                            byte[] fileContents = memoryStream.ToArray();

                            // Provide the PDF file for download
                            return File(fileContents, "application/pdf", "EventsDetails.pdf");
                        }
                    }
                }
            }

            return RedirectToAction("ReportsPage", "Reports", new
            {
                id = id
            });
        }

        public ActionResult DownloadGameRatingsDetails(int id = 0)
        {
            string query = "SELECT Rating.Id, Rating.RatingValue, Rating.UserProfileId, Rating.GameId, Games.Name AS Gamename, Profiles.DisplayName AS Username " +
                           "FROM Rating " +
                           "INNER JOIN Games ON Rating.GameId = Games.Id " +
                           "INNER JOIN Profiles ON Rating.UserProfileId = Profiles.Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var records = new List<EachRatingVM>();

                        while (reader.Read())
                        {
                            var record = new EachRatingVM
                            {
                            
                                RatingValue = reader.GetInt32(reader.GetOrdinal("RatingValue")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                GameName = reader.GetString(reader.GetOrdinal("GameName")),
                            };

                            records.Add(record);
                        }

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Create a PDF document
                            using (PdfWriter writer = new PdfWriter(memoryStream))
                            {
                                using (PdfDocument pdf = new PdfDocument(writer))
                                {
                                    Document document = new Document(pdf);

                                    // Add a title to the PDF
                                    document.Add(new Paragraph("Details of Ratings for Each Game by Each User"));

                                    // Add rating details to the PDF
                                    foreach (var record in records)
                                    {
                                        document.Add(new Paragraph($"User: {record.UserName}, Game: {record.GameName}, Rating: {record.RatingValue}"));
                                    }
                                }
                            }

                            // Convert the PDF document to bytes
                            byte[] fileContents = memoryStream.ToArray();

                            // Provide the PDF file for download
                            return File(fileContents, "application/pdf", "GameRatingsDetails.pdf");
                        }
                    }
                }
            }

            return RedirectToAction("ReportsPage", "Reports", new
            {
                id = id
            });
        }


        public ActionResult DownloadGameReviewsDetails(int id = 0)
        {
            string query = "SELECT Review.Id, Review.ReviewText, Review.IsReviewed, Review.ReviewBy, " +
                           "Review.UserProfileId, Review.GameId, Games.Name AS GameName, Profiles.DisplayName As Username " +
                           "FROM Review " +
                           "INNER JOIN Games ON Review.GameId = Games.Id " +
                           "INNER JOIN Profiles ON Review.UserProfileId = Profiles.Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var records = new List<EachReviewVM>();

                        while (reader.Read())
                        {
                            var record = new EachReviewVM
                            {
                                ReviewText = reader.GetString(reader.GetOrdinal("ReviewText")),
                                IsReviewed = reader.GetBoolean(reader.GetOrdinal("IsReviewed")),
                                ReviewBy = reader.GetString(reader.GetOrdinal("ReviewBy")),
                                GameName = reader.GetString(reader.GetOrdinal("GameName")),
                            };

                            records.Add(record);
                        }

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Create a PDF document
                            using (PdfWriter writer = new PdfWriter(memoryStream))
                            {
                                using (PdfDocument pdf = new PdfDocument(writer))
                                {
                                    Document document = new Document(pdf);

                                    // Add a title to the PDF
                                    document.Add(new Paragraph("Details of Reviews for Each Game"));

                                    // Add review details to the PDF
                                    foreach (var record in records)
                                    {
                                        document.Add(new Paragraph($"User: {record.ReviewBy}, Game: {record.GameName}, " +
                                            $"Review: {record.ReviewText}, Reviewed: {record.IsReviewed}, Review By: {record.ReviewBy}"));
                                    }
                                }
                            }

                            // Convert the PDF document to bytes
                            byte[] fileContents = memoryStream.ToArray();

                            // Provide the PDF file for download
                            return File(fileContents, "application/pdf", "GameReviewsDetails.pdf");
                        }
                    }
                }
            }

            return RedirectToAction("ReportsPage", "Reports", new
            {
                id = id
            });
        }

        public ActionResult DownloadEventParticipantsDetails(int id = 0)
        {
            string query = "SELECT AllParticipations.Id, AllParticipations.EventId, AllParticipations.UserProfileId, " +
                           "Profiles.DisplayName AS UserName, Events.Name AS EventName " +
                           "FROM AllParticipations " +
                           "INNER JOIN Profiles ON AllParticipations.UserProfileId = Profiles.Id " +
                           "INNER JOIN Events ON AllParticipations.EventId = Events.Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var records = new List<EventParticipation>();

                        while (reader.Read())
                        {
                            var record = new EventParticipation
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                EventId = reader.GetInt32(reader.GetOrdinal("EventId")),
                                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                Profile = new UserProfile
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                    DisplayName = reader.GetString(reader.GetOrdinal("UserName"))
                                },
                                Event = new Event
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("EventId")),
                                    Name = reader.GetString(reader.GetOrdinal("EventName"))
                                }
                            };

                            records.Add(record);
                        }

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Create a PDF document
                            using (PdfWriter writer = new PdfWriter(memoryStream))
                            {
                                using (PdfDocument pdf = new PdfDocument(writer))
                                {
                                    Document document = new Document(pdf);

                                    // Add a title to the PDF
                                    document.Add(new Paragraph("Details of all Event Participants"));

                                    // Add event participants details to the PDF
                                    foreach (var record in records)
                                    {
                                        document.Add(new Paragraph($"Event: {record.Event.Name}, " +
                                            $"Participant: {record.Profile.DisplayName}"));
                                    }
                                }
                            }

                            // Convert the PDF document to bytes
                            byte[] fileContents = memoryStream.ToArray();

                            // Provide the PDF file for download
                            return File(fileContents, "application/pdf", "EventParticipantsDetails.pdf");
                        }
                    }
                }
            }

            return RedirectToAction("ReportsPage", "Reports", new
            {
                id = id
            });
        }


        public ActionResult DownloadUserAddressesDetails(int id = 0)
        {
            string query = "SELECT Profiles.Id AS UserId, Profiles.DisplayName AS UserName, " +
                           "UserAddresses.Id, UserAddresses.FirstName, UserAddresses.LastName, UserAddresses.PhoneNumber, " +
                           "UserAddresses.StreetAddress, UserAddresses.AptNumber, UserAddresses.City, UserAddresses.Province, " +
                           "UserAddresses.PostalCode, UserAddresses.DeliveryInstructions, " +
                           "UserAddresses.StreetAddressShipping, UserAddresses.AptNumberShipping, " +
                           "UserAddresses.CityShipping, UserAddresses.ProvinceShipping, UserAddresses.PostalCodeShipping, " +
                           "UserAddresses.SameAsShipping " +
                           "FROM Profiles " +
                           "INNER JOIN UserAddresses ON Profiles.Id = UserAddresses.UserProfileId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var records = new List<(UserProfile UserProfile, UserAddress UserAddress)>();

                        while (reader.Read())
                        {
                            var userProfile = new UserProfile
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                                DisplayName = reader.GetString(reader.GetOrdinal("UserName"))
                            
                            };

                            var userAddress = new UserAddress
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                StreetAddress = reader.GetString(reader.GetOrdinal("StreetAddress")),
                                City = reader.GetString(reader.GetOrdinal("City")),
                                Province = reader.GetString(reader.GetOrdinal("Province")),
                                PostalCode = reader.GetString(reader.GetOrdinal("PostalCode")),
                                DeliveryInstructions = reader.GetString(reader.GetOrdinal("DeliveryInstructions")),
                                StreetAddressShipping = reader.GetString(reader.GetOrdinal("StreetAddressShipping")),
                                CityShipping = reader.GetString(reader.GetOrdinal("CityShipping")),
                                ProvinceShipping = reader.GetString(reader.GetOrdinal("ProvinceShipping")),
                                PostalCodeShipping = reader.GetString(reader.GetOrdinal("PostalCodeShipping")),
                                SameAsShipping = reader.GetBoolean(reader.GetOrdinal("SameAsShipping"))
                            };

                            records.Add((userProfile, userAddress));
                        }

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Create a PDF document
                            using (PdfWriter writer = new PdfWriter(memoryStream))
                            {
                                using (PdfDocument pdf = new PdfDocument(writer))
                                {
                                    Document document = new Document(pdf);

                                    // Add a title to the PDF
                                    document.Add(new Paragraph("Details of All Addresses"));

                                    // Add user address details to the PDF
                                    foreach (var (userProfile, userAddress) in records)
                                    {
                                        document.Add(new Paragraph($"User Name: {userProfile.DisplayName}, " +
                                            $"Street Address: {userAddress.StreetAddress}, " +
                                            $"City: {userAddress.City}, " +
                                            $"Postal Code: {userAddress.PostalCode}, " +
                                            $"Province: {userAddress.Province}, "+
                                            $"Phone Number: {userAddress.Province}, ")
                                         );
                                    }
                                }
                            }

                            // Convert the PDF document to bytes
                            byte[] fileContents = memoryStream.ToArray();

                            // Provide the PDF file for download
                            return File(fileContents, "application/pdf", "UserAddressesDetails.pdf");
                        }
                    }
                }
            }

            return RedirectToAction("ReportsPage", "Reports", new
            {
                id = id
            });
        }




        private AppDbContext _appDbContext;
    }
}
