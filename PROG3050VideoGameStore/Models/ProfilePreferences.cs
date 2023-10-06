using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace PROG3050VideoGameStore.Models
{
    public class ProfilePreferences
    {
        public int Id { get; set; }

        [Required]
        public string FavCategory { get; set; }
        [Required]
        public string FavPlatform { get; set; }
        [Required]
        public string Language { get; set; }
 
        public int ProfileId { get; set; } // Required foreign key property
        public UserProfile Profile { get; set; } = null!; // Required reference navigation to principal

    }
}
