using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int RatingValue { get; set; }

        public int UserProfileId { get; set; } // Foreign Key

        public int GameId { get; set; } // Foreign Key

        public UserProfile? Profile { get; set; } // nav property

        public Game? Game { get; set; } // nav property
    }
}
