using System.Transactions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PROG3050VideoGameStore.Models
{
    public class UserProfile
    {


        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; } //This is can be used as username


        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public string ActualName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public Boolean PromotionEmail { get; set; } = false;

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Boolean EmailValidate { get; set; } = false;

        public Boolean IsEmployee { get; set; } = false;

        [Display(Name = "Remember me")]
        public bool? RememberMe { get; set; } = false;

        public ProfilePreferences? Preferences { get; set; } // nav to its child preferences

        public UserAddress? Address { get; set; } //nav to its child address

    }
}
