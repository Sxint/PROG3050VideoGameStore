using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class DetailsForFriendsWishListVM
    {
        public int ProfileId { get; set; }

        public int FriendId { get; set; }
        public Game ActiveGame { get; set; }

        public ProfilePreferences CurrentUserPreference { get; set; }

        public List<Game> GameRecommendations { get; set; }
    }
}
