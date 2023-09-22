using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class AccountInfoViewModel
    {
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
    }
}