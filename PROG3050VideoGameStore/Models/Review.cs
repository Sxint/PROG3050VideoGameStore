using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string ReviewText { get; set; }

        public bool IsReviewed { get; set; } = false;

        public string ReviewBy { get; set; }

        public int ProfileId { get; set; } // Foreign Key

        public int GameId { get; set; } // Foreign Key

        public UserProfile? Profile { get; set; } // nav property

        public Game? Game { get; set; } // nav property
    }
}
