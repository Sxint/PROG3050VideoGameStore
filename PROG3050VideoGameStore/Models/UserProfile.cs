using System.Transactions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PROG3050VideoGameStore.ViewModels;

namespace PROG3050VideoGameStore.Models
{
    public class UserProfile
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "Display name is required")]
        [StringLength(50)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; } //This is can be used as username

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("(^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$)?(^(?=.*\\d)(?=.*[a-z])(?=.*[@#$%^&+=]).{8,32}$)?(^(?=.*\\d)(?=.*[A-Z])(?=.*[@#$%^&+=]).{6,32}$)?(^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{8,32}$)?", ErrorMessage = "Password doesnt meet the criteria")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Actual name is required")]
        [Display(Name = "Actual Name")]
        public string ActualName { get; set; } = "Not yet provided";

        [Required(ErrorMessage ="Gender field is required")]
        
        public string Gender { get; set; } = "Not yet provided";

        [Required(ErrorMessage ="Birthdate needs to be provided")]
        [Display(Name = "Birth Date")]
        [DateNotInFuture(ErrorMessage = "Birth Date cannot be in the future")]
        public DateTime BirthDate { get; set; } = DateTime.Now.Date;

        [Display(Name = "Promotional Email")]
        public Boolean PromotionEmail { get; set; } = false;

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage ="Please enter a proper email address")]

        public string Email { get; set; }

        [Display(Name = "Email Validate")]
        public Boolean EmailValidate { get; set; } = false;

        public Boolean IsEmployee { get; set; } = false;

        [Display(Name = "Remember me")]
        public bool? RememberMe { get; set; } = false;

        public int? CurrentPrefId { get; set; }

        public int? CurrentAddressId { get; set; }

        public int RepeatedInvalidCreds { get; set; } = 0;

        public ProfilePreferences? Preferences { get; set; } // nav to its child preferences

        public UserAddress? Address { get; set; } //nav to its child address

        public Rating? RatingItem { get; set; } //nav to its child rating

        public Review? ReviewItem { get; set; } // nav to its review item


    }
}
