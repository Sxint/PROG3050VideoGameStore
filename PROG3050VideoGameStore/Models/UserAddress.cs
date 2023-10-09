using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class UserAddress
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = "Not provided yet";

        [Required]
        public string LastName { get; set; } = "Not provided yet";

        [Phone, Required]
        public string PhoneNumber { get; set; } = "Not provided yet";

        [Required]
        public string StreetAddress { get; set; } = "Not provided yet";


        public string? AptNumber { get; set; } = "Not Available";

        [Required]
        public string City { get; set; } = "Not provided yet";

        [Required]
        public string Province { get; set; } = "Not provided yet";

        [Required]
        public string PostalCode { get; set; } = "Not provided yet";

        [Required]
        public string DeliveryInstructions { get; set; } = "Not provided yet";

        public int ProfileId { get; set; } // Required foreign key property
        public UserProfile? Profile { get; set; } // Required reference navigation to principal
    }
}
