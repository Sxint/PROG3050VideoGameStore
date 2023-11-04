using System.ComponentModel.DataAnnotations;
namespace PROG3050VideoGameStore.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Category { get; set; } = "None";
        [Required]
        public string Platform { get; set; } = "None";

        public Rating? RatingItem { get; set; } //nav to its child rating

        public Review? ReviewItem { get; set; } // nav to its review item
    }
}
