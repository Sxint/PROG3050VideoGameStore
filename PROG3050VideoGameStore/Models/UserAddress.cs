using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class UserAddress
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone, Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string StreetAddress { get; set; }


        public string? AptNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string DeliveryInstructions { get; set; }

        [Required]
        public string StreetAddressShipping { get; set; }


        public string? AptNumberShipping { get; set; }

        [Required]
        public string CityShipping { get; set; }

        [Required]
        public string ProvinceShipping { get; set; }

        [Required]
        public string PostalCodeShipping { get; set; }

        public bool SameAsShipping { get; set; }

        public int UserProfileId { get; set; } // Required foreign key property
        public UserProfile? Profile { get; set; } // Required reference navigation to principal
    }
}
