using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class UserAddress
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Phone, Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Apartment Number(Optional)")]
        public string? AptNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Delivery Instructions")]
        public string DeliveryInstructions { get; set; }

        [Required]
        [Display(Name = "Street Address Shipping")]
        public string StreetAddressShipping { get; set; }

        [Display(Name = "Apt Number Shipping(Optional)")]
        public string? AptNumberShipping { get; set; }

        [Required]
        [Display(Name = "City Shipping")]
        public string CityShipping { get; set; }

        [Required]
        [Display(Name = "Province Shipping")]
        public string ProvinceShipping { get; set; }

        [Required]
        [Display(Name = "Postal Code Shipping")]
        public string PostalCodeShipping { get; set; }

        [Display(Name = "Same as shipping")]
        public bool SameAsShipping { get; set; }

        public int UserProfileId { get; set; } // Required foreign key property
        public UserProfile? Profile { get; set; } // Required reference navigation to principal
    }
}
