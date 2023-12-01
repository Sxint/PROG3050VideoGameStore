using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class OrderConfirmationVM
    {
        public Order Order { get; set; }

        public int ProfileId { get; set; }

        public UserAddress Address { get; set; }

        public List<Game> PurchasedGames { get; set; }
    }
}
