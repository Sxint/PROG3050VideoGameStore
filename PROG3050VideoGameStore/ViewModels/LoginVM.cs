using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Please enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Please enter a password")]
        public string Password { get; set; }


    }
}
