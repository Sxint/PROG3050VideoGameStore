namespace PROG3050VideoGameStore.Models
{
    public class WishList
    {

        public int Id { get; set; }

        public int GameId { get; set; }

        public int UserProfileId { get; set; }

        public Game? Game { get; set; } // nav property

        public UserProfile? Profile { get; set; } // nav property
    }
}
