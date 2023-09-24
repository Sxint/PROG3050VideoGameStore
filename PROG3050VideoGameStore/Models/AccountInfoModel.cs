using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PROG3050VideoGameStore.Models
{
    public class AccountInfoViewModel
    {
        //Vital Account information
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)] 
        public string Password { get; set; }


        //Profile & Preferences
        public string? Name { get; set; }

        public string? Gender { get; set; }

        public DateOnly? Birthday { get; set; }

        public bool? ReceiveEmails { get; set; }

        public string? Platforms { get; set; } //set this to list later

        public string? Genres { get; set; } //set this to list later

        public string? Language { get; set; } //set this to list later


        //Address
        public string FirstName { get; set; }

        public string LastName { get; set; }


        [Phone] 
        public string PhoneNumber { get; set; }

        public string StreetAddress { get; set; }

        public int AptNumber { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public string DeliveryInstructions { get; set; }
    }
}