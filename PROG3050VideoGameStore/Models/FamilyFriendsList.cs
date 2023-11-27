using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class FamilyFriendsList
    {
        public int Id { get; set; }

        public int UserProfileId { get; set; }

        public int ReceivedBy { get; set; } = 0;

        [Required]
        [Display(Name = "Recepient Name")]
        public string RecepientName { get; set; }

        [Required]
        [Display(Name = "Relationship")]
        public string Relationship { get; set; }

        public string Status { get; set; } = "Not yet accepted";

        public UserProfile? Profile { get; set; } // nav property
    }
}
