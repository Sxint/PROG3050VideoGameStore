using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class UserAddress:IdentityUser
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

        public int ProfileId { get; set; } // Required foreign key property
        public UserProfile Profile { get; set; } = null!; // Required reference navigation to principal
    }
}
