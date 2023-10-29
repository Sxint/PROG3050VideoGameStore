using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class ReviewListVM
    {
        public int ProfileId { get; set; }

        public int GameId { get; set; }

        public List<Review> AllReviews { get; set; }
    }
}
