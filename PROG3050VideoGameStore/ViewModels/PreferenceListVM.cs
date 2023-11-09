using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class PreferenceListVM
    {
        public List<ProfilePreferences>? AllPreferences { get; set; }

        public int? ProfileId { get; set; }
    }
}
