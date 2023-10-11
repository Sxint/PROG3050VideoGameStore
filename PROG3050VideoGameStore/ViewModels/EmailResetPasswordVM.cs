using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.ViewModels
{
    public class EmailResetPasswordVM
    {
        [Required]
        public string? UserName { get; set; }
    }
}
