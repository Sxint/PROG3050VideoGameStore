using PROG3050VideoGameStore.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Card Holder Name is required")]
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }


        [Required(ErrorMessage = "The Card Number is required")]
        [Display(Name = "Card Number")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "The Card Number must be 16 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The Card Number must contain only numeric characters")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage ="Expiry Date is required")]
        [Display(Name = "Expiry Date")]
        [ExpiryDateCheck(ErrorMessage = "The Expiry Date is invalid")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVC Number is required")]
        [Display(Name = "CVC Number")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CVC Number must be 3 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The CVC Number must contain only numeric characters")]
        public string CVCNumber { get; set; }


        public string OrderStatus { get; set; } = "Not Confirmed";

        public double? TotalCost { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public int UserProfileId { get; set; } // Required foreign key property
        public UserProfile? Profile { get; set; } // Required reference navigation to principal
    }
}
