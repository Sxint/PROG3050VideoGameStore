using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class AllFriendsFamilyVM
    {
        public List<FamilyFriendsList> AllFriendsList { get; set; }

        public List<UserProfile> AllProfiles { get; set; }

        public int ProfileId { get; set; }

    }
}
