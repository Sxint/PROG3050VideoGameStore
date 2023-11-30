using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class Cart
    {
       
        public int Id { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public int UserProfileId { get; set; } // Required foreign key property
        public UserProfile? Profile { get; set; } // Required reference navigation to principal


    }
}
