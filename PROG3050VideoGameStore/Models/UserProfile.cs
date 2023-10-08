using System.Transactions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PROG3050VideoGameStore.Models
{
    public class UserProfile
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "Display name is required")]
        [StringLength(50)]
        public string DisplayName { get; set; } //This is can be used as username


        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("(^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$)?(^(?=.*\\d)(?=.*[a-z])(?=.*[@#$%^&+=]).{8,32}$)?(^(?=.*\\d)(?=.*[A-Z])(?=.*[@#$%^&+=]).{6,32}$)?(^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{8,32}$)?", ErrorMessage = "Password doesnt meet the criteria")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Actual name is required")]
        public string ActualName { get; set; } = "Not yet provided";

        [Required(ErrorMessage ="Gender field is required")]
        public string Gender { get; set; } = "N/A";

        [Required(ErrorMessage ="Birthdate needs to be provided")]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        
        public Boolean PromotionEmail { get; set; } = false;

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage ="Please enter a proper email address")]
        public string Email { get; set; } 

        public Boolean EmailValidate { get; set; } = false;

        public Boolean IsEmployee { get; set; } = false;

        [Display(Name = "Remember me")]
        public bool? RememberMe { get; set; } = false;

        public int RepeatedInvalidCreds { get; set; } = 0;

        public ProfilePreferences? Preferences { get; set; } // nav to its child preferences

        public UserAddress? Address { get; set; } //nav to its child address

    }
}
