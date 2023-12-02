using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class RatingVM
    {
        public int GameId { get; set; }

        public int ProfileId { get; set; }

        public Game? Game { get; set; }

        public Rating NewRating { get; set; }

        public List<Rating>? RatingList { get; set; }

    }
}
