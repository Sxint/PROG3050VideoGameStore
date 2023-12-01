using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class OrderVM
    {
        public int? ProfileId { get; set; }

        public Order? Order { get; set; }
    }
}
