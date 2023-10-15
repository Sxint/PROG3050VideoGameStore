using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.ViewModels
{
    public class ChangePasswordVM
    {
        [Required]
        public string OldPassword { get; set; }

        public string? ObtainedOldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmNewPassword { get; set; }

        public int? CurrentProfileId { get; set; }
    }
}
