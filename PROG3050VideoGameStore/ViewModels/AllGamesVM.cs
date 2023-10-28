using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class AllGamesVM
    {
        public List<Game> AllGames { get; set; }

        public int ProfileId { get; set; }

        public List<Rating> Ratings { get; set; }
    }
}
