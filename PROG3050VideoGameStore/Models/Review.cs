using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Review Text")]
        public string ReviewText { get; set; }

        public bool IsReviewed { get; set; } = false;
        
        [Display(Name = "Review By")]
        public string ReviewBy { get; set; }

        public int UserProfileId { get; set; } // Foreign Key

        public int GameId { get; set; } // Foreign Key

        public UserProfile? Profile { get; set; } // nav property

        public Game? Game { get; set; } // nav property
    }
}
