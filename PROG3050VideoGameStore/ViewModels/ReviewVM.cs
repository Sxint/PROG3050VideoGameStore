using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class ReviewVM
    {
        public int GameId { get; set; }

        public int ProfileId { get; set; }

        public Game? Game { get; set; }

        public Review NewReview { get; set; }

        public List<Review>? ReviewList { get; set; }
    }
}
