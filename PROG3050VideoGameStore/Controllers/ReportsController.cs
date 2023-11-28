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

         
        

           
        



        private AppDbContext _appDbContext;
    }
}
