using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class ProfileVM
    {
        public List<Game> AllGames { get; set; }

        public List<FamilyFriendsList> AllFriendsList { get; set; }

        public List<UserProfile> AllProfiles { get; set; }

        public ProfilePreferences Preference { get; set; }

        public int ProfileId { get; set; }
    }
}
