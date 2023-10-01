using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class UserAddress
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone, Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public int AptNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string DeliveryInstructions { get; set; }
    }
}
